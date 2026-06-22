using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Content;
using Portfolio_ZoranSimeunovic.Data;
using Portfolio_ZoranSimeunovic.Models;

namespace Portfolio_ZoranSimeunovic.Controllers;

public class QuestionnaireController : Controller
{
    private readonly AppDbContext _db;
    private readonly MsGraphClient.MsGraphClient? _graph;
    private readonly IConfiguration _config;
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
        IConfiguration config,
        ILogger<QuestionnaireController> logger,
        IWebHostEnvironment env,
        MsGraphClient.MsGraphClient? graph = null)
    {
        _db = db;
        _config = config;
        _logger = logger;
        _env = env;
        _graph = graph;
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

        ViewBag.Token = token;
        ViewBag.Name = q.ContactLead.Name;
        ViewBag.Stage = q.Stage;
        ViewBag.Step1 = q.Step1Answers;
        ViewBag.Step2 = q.Step2Answers;
        ViewBag.Step3 = q.Step3Answers;

        return View();
    }

    [HttpGet("/questionnaire/test-seed")]
    public async Task<IActionResult> TestSeed([FromServices] IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
            return NotFound();

        const string token = "test-token-zoran-2026";

        var existing = await _db.Questionnaires.FirstOrDefaultAsync(x => x.Token == token);
        if (existing != null)
        {
            existing.CompletedAt = null;
            existing.Stage = 0;
            existing.Step1Answers = null;
            existing.Step2Answers = null;
            existing.Step3Answers = null;
            existing.TokenExpiresAt = DateTime.UtcNow.AddDays(30);
            await _db.SaveChangesAsync();
            return Redirect($"/questionnaire?token={token}");
        }

        var lead = new ContactLead
        {
            Name = "Zoran Test",
            Email = "zoransimeunovic@outlook.de",
            Language = "de",
            CreatedAt = DateTime.UtcNow
        };
        _db.ContactLeads.Add(lead);
        await _db.SaveChangesAsync();

        _db.Questionnaires.Add(new Questionnaire
        {
            ContactLeadId = lead.Id,
            Token = token,
            TokenExpiresAt = DateTime.UtcNow.AddDays(30),
            Stage = 0,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        return Redirect($"/questionnaire?token={token}");
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
                q.Stage = 3;
                q.CompletedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                await SendCompletionEmailAsync(q);
                return Ok(new { success = true });
            default:
                return BadRequest();
        }

        await _db.SaveChangesAsync();
        return Ok(new { success = true });
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

    private async Task SendCompletionEmailAsync(Questionnaire q)
    {
        if (_graph is null) return;

        try
        {
            var lead = await _db.ContactLeads.FindAsync(q.ContactLeadId);
            if (lead is null) return;

            var ownerEmail = _config["Notification:OwnerEmail"] ?? "";
            if (string.IsNullOrWhiteSpace(ownerEmail)) return;

            var body = BuildNotificationEmail(q, lead);
            var result = await _graph.SendEmailAsync(
                ownerEmail, "Zoran",
                $"[Portfolio] Novi upitnik — {lead.Name}",
                body);

            if (!result.Success)
                _logger.LogWarning("Email notifikacija nije poslana za lead {LeadId}: {Error}", q.ContactLeadId, result.Error);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Greška pri slanju email notifikacije za lead {LeadId}", q.ContactLeadId);
        }
    }

    private static string BuildNotificationEmail(Questionnaire q, ContactLead lead)
    {
        var s1 = ParseJson(q.Step1Answers);
        var s2 = ParseJson(q.Step2Answers);
        var s3 = ParseJson(q.Step3Answers);

        var industry = s1.GetValueOrDefault("industry", "");
        if (industry == "__other__") industry = s1.GetValueOrDefault("industryOther", "");
        var projectType = s2.GetValueOrDefault("websiteType", "");
        var extras = s2.GetValueOrDefault("projectExtra", "");
        var goals = s3.GetValueOrDefault("goals", "");
        var startWhen = s3.GetValueOrDefault("startWhen", "");
        var problem = s3.GetValueOrDefault("biggestProblem", "");

        var sb = new StringBuilder();
        sb.AppendLine("Upitnik završen!");
        sb.AppendLine();
        sb.AppendLine($"Ime:       {lead.Name}");
        sb.AppendLine($"Email:     {lead.Email}");
        sb.AppendLine($"Datum:     {q.CompletedAt:dd.MM.yyyy HH:mm} UTC");
        sb.AppendLine();
        if (!string.IsNullOrWhiteSpace(industry))
            sb.AppendLine($"Industrija:      {industry}");
        if (!string.IsNullOrWhiteSpace(projectType))
            sb.AppendLine($"Vrsta projekta:  {projectType}");
        if (!string.IsNullOrWhiteSpace(extras))
            sb.AppendLine($"Dodatno:         {extras}");
        if (!string.IsNullOrWhiteSpace(goals))
            sb.AppendLine($"Ciljevi:         {goals}");
        if (!string.IsNullOrWhiteSpace(startWhen))
            sb.AppendLine($"Početak:         {startWhen}");
        if (!string.IsNullOrWhiteSpace(problem))
        {
            sb.AppendLine();
            sb.AppendLine($"Najveći problem: {problem}");
        }
        sb.AppendLine();
        sb.AppendLine("Sve detalje pogledaj u admin panelu.");

        return sb.ToString();
    }

    private static Dictionary<string, string> ParseJson(string? json)
    {
        var result = new Dictionary<string, string>(StringComparer.Ordinal);
        if (string.IsNullOrWhiteSpace(json)) return result;
        try
        {
            using var doc = JsonDocument.Parse(json);
            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                var val = prop.Value.ValueKind == JsonValueKind.Array
                    ? string.Join(", ", prop.Value.EnumerateArray().Select(x => x.GetString() ?? ""))
                    : prop.Value.GetString() ?? "";
                if (!string.IsNullOrWhiteSpace(val))
                    result[prop.Name] = val;
            }
        }
        catch { }
        return result;
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
