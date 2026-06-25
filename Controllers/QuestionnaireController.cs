using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Content;
using Portfolio_ZoranSimeunovic.Data;
using Portfolio_ZoranSimeunovic.Models;

namespace Portfolio_ZoranSimeunovic.Controllers;

public class QuestionnaireController : Controller
{
    private readonly AppDbContext _db;
    private readonly ILogger<QuestionnaireController> _logger;
    private readonly IWebHostEnvironment _env;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf", ".doc", ".docx", ".txt", ".jpg", ".jpeg", ".png", ".gif",
        ".webp", ".svg", ".zip", ".rar", ".ai", ".eps"
    };

    private const long MaxFileSizeBytes = 10 * 1024 * 1024;

    public QuestionnaireController(
        AppDbContext db,
        ILogger<QuestionnaireController> logger,
        IWebHostEnvironment env)
    {
        _db = db;
        _logger = logger;
        _env = env;
    }

    [HttpGet("/questionnaire/dev")]
    public async Task<IActionResult> DevOpen()
    {
        if (!_env.IsDevelopment()) return NotFound();

        var lead = await _db.ContactLeads.FirstOrDefaultAsync(x => x.Email == "dev@test.local");
        if (lead == null)
        {
            lead = new ContactLead { Name = "Dev Test", Email = "dev@test.local", CreatedAt = DateTime.UtcNow, EmailConfirmedAt = DateTime.UtcNow };
            _db.ContactLeads.Add(lead);
            await _db.SaveChangesAsync();
        }

        var q = await _db.Questionnaires.FirstOrDefaultAsync(x => x.ContactLeadId == lead.Id && !x.CompletedAt.HasValue);
        if (q == null)
        {
            q = new Questionnaire
            {
                ContactLeadId = lead.Id,
                Token = Guid.NewGuid().ToString("N"),
                TokenExpiresAt = DateTime.UtcNow.AddDays(30),
                CreatedAt = DateTime.UtcNow
            };
            _db.Questionnaires.Add(q);
            await _db.SaveChangesAsync();
        }

        return Redirect("/questionnaire?token=" + q.Token);
    }

    [HttpGet("/questionnaire")]
    public async Task<IActionResult> Index(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return NotFound();

        var q = await _db.Questionnaires
            .Include(x => x.ContactLead)
            .FirstOrDefaultAsync(x => x.Token == token);

        if (q == null || q.TokenExpiresAt < DateTime.UtcNow)
            return View("Expired");

        if (q.CompletedAt.HasValue)
            return View("AlreadyCompleted");

        Response.Cookies.Append("q_ref", token, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            IsEssential = true,
            Path = "/",
            SameSite = SameSiteMode.Lax
        });

        ViewBag.Token = token;
        ViewBag.QuestionnaireToken = token;
        ViewBag.Name = q.ContactLead.Name;
        ViewBag.Stage = q.Stage;
        ViewBag.Step1 = q.Step1Answers;
        ViewBag.Step2 = q.Step2Answers;
        ViewBag.Step3 = q.Step3Answers;

        return View();
    }

[HttpPost("/questionnaire/opt-out")]
    public async Task<IActionResult> OptOut([FromBody] OptOutRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            return BadRequest();

        var q = await _db.Questionnaires
            .Include(x => x.ContactLead)
            .FirstOrDefaultAsync(x => x.Token == request.Token);

        if (q == null || q.TokenExpiresAt < DateTime.UtcNow)
            return BadRequest();

        q.ContactLead.OptedOut = true;
        await _db.SaveChangesAsync();

        Response.Cookies.Delete("q_ref");

        return Ok(new { success = true });
    }

    [HttpPost("/questionnaire/save-step")]
    public async Task<IActionResult> SaveStep([FromBody] SaveStepRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            return BadRequest();

        var q = await _db.Questionnaires
            .FirstOrDefaultAsync(x => x.Token == request.Token);

        if (q == null || q.TokenExpiresAt < DateTime.UtcNow || q.CompletedAt.HasValue)
            return BadRequest();

        switch (request.Step)
        {
            case 1:
                q.Step1Answers = request.Answers;
                if (q.Stage < 1) q.Stage = 1;
                break;
            case 2:
                q.Step2Answers = request.Answers;
                if (q.Stage < 2) q.Stage = 2;
                break;
            case 3:
                q.Step3Answers = request.Answers;
                if (q.Stage < 3) q.Stage = 3;
                break;
            case 4:
                q.Step4Answers = request.Answers;
                if (q.Stage < 4) q.Stage = 4;
                break;
            case 5:
                q.Step5Answers = request.Answers;
                q.Stage = 5;
                q.CompletedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                Response.Cookies.Delete("q_ref");
                return Ok(new { success = true });
            default:
                return BadRequest();
        }

        await _db.SaveChangesAsync();
        return Ok(new { success = true });
    }

    [HttpGet("/questionnaire/done")]
    public async Task<IActionResult> Done(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return RedirectToAction("Index", "Home");

        var q = await _db.Questionnaires
            .Include(x => x.ContactLead)
            .FirstOrDefaultAsync(x => x.Token == token && x.CompletedAt.HasValue);

        if (q == null)
            return RedirectToAction("Index", "Home");

        ViewBag.Name = q.ContactLead.Name;
        return View("Done");
    }

    [HttpPost("/questionnaire/upload-file")]
    [RequestSizeLimit(MaxFileSizeBytes + 1024)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSizeBytes + 1024)]
    public async Task<IActionResult> UploadFile([FromForm] string token, [FromForm] string fileLabel, IFormFile? file)
    {
        if (string.IsNullOrWhiteSpace(token) || file is null || file.Length == 0)
            return BadRequest(new { success = false, error = "Nedostaju podaci." });

        var q = await _db.Questionnaires
            .FirstOrDefaultAsync(x => x.Token == token);

        if (q == null || q.TokenExpiresAt < DateTime.UtcNow)
            return BadRequest(new { success = false, error = "Neispravan ili istekao token." });

        if (file.Length > MaxFileSizeBytes)
            return BadRequest(new { success = false, error = "Fajl je prevelik. Maksimalna veličina je 10 MB." });

        var ext = Path.GetExtension(file.FileName);
        if (!AllowedExtensions.Contains(ext))
            return BadRequest(new { success = false, error = $"Tip fajla '{ext}' nije dozvoljen." });

        var storedName = $"{Guid.NewGuid():N}{ext}";
        var uploadsPath = Path.Combine(_env.ContentRootPath, "PrivateUploads");
        Directory.CreateDirectory(uploadsPath);

        await using (var stream = System.IO.File.Create(Path.Combine(uploadsPath, storedName)))
            await file.CopyToAsync(stream);

        _db.QuestionnaireFiles.Add(new QuestionnaireFile
        {
            QuestionnaireId = q.Id,
            FileLabel = fileLabel.Trim(),
            OriginalFileName = file.FileName,
            StoredFileName = storedName,
            ContentType = file.ContentType ?? "application/octet-stream",
            SizeBytes = file.Length,
            UploadedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        return Ok(new { success = true, fileName = file.FileName });
    }


}

public class OptOutRequest
{
    public string Token { get; set; } = string.Empty;
}

public class SaveStepRequest
{
    public string Token { get; set; } = string.Empty;
    public int Step { get; set; }
    public string Answers { get; set; } = string.Empty;
}
