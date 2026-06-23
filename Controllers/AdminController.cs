using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Content;
using Portfolio_ZoranSimeunovic.Data;
using Portfolio_ZoranSimeunovic.Models;

namespace Portfolio_ZoranSimeunovic.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
        ".txt", ".csv", ".jpg", ".jpeg", ".png", ".gif", ".zip", ".rar"
    };

    private const long MaxFileSizeBytes = 10 * 1024 * 1024;

    public AdminController(AppDbContext db, IConfiguration config, IWebHostEnvironment env)
    {
        _db = db;
        _config = config;
        _env = env;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return Redirect("/admin");
        return View();
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginPost([FromForm] string username, [FromForm] string password)
    {
        var expectedUsername = _config["Admin:Username"] ?? "";
        var expectedHash = _config["Admin:PasswordHash"] ?? "";

        if (string.IsNullOrWhiteSpace(expectedHash) ||
            !string.Equals(username, expectedUsername, StringComparison.Ordinal) ||
            !string.Equals(Sha256(password), expectedHash, StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Pogrešno korisničko ime ili lozinka.";
            return View("Login");
        }

        var identity = new ClaimsIdentity(
            [new Claim(ClaimTypes.Name, username)],
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = true });

        return Redirect("/admin");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/admin/login");
    }

    [Authorize]
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var list = await _db.Questionnaires
            .Include(q => q.ContactLead)
            .OrderByDescending(q => q.CreatedAt)
            .Select(q => new QuestionnaireListItem
            {
                Id = q.Id,
                ContactLeadId = q.ContactLeadId,
                ClientName = q.ContactLead.Name,
                ClientEmail = q.ContactLead.Email,
                Stage = q.Stage,
                CompletedAt = q.CompletedAt,
                CreatedAt = q.CreatedAt,
                OptedOut = q.ContactLead.OptedOut,
                OfferSentAt = q.ContactLead.OfferSentAt,
                CompletionNotified = q.CompletionNotificationSentAt.HasValue
            })
            .ToListAsync();

        return View(list);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var q = await _db.Questionnaires
            .Include(x => x.ContactLead)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (q is null) return NotFound();

        var files = await _db.QuestionnaireFiles
            .Where(f => f.QuestionnaireId == id)
            .OrderBy(f => f.UploadedAt)
            .Select(f => new QuestionnaireFileItem
            {
                Id = f.Id,
                FileLabel = f.FileLabel,
                OriginalFileName = f.OriginalFileName,
                SizeBytes = f.SizeBytes,
                UploadedAt = f.UploadedAt
            })
            .ToListAsync();

        var vm = new QuestionnaireDetailVm
        {
            Id = q.Id,
            ContactLeadId = q.ContactLeadId,
            ClientName = q.ContactLead.Name,
            ClientEmail = q.ContactLead.Email,
            Token = q.Token,
            CreatedAt = q.CreatedAt,
            CompletedAt = q.CompletedAt,
            Stage = q.Stage,
            OptedOut = q.ContactLead.OptedOut,
            OfferSentAt = q.ContactLead.OfferSentAt,
            Step1 = ParseAnswers(q.Step1Answers),
            Step2 = ParseAnswers(q.Step2Answers),
            Step3 = ParseAnswers(q.Step3Answers),
            Files = files
        };

        return View(vm);
    }

    [Authorize]
    [HttpGet("qfile/{id:int}/download")]
    public async Task<IActionResult> QFileDownload(int id)
    {
        var f = await _db.QuestionnaireFiles.FindAsync(id);
        if (f is null) return NotFound();

        var path = Path.Combine(_env.ContentRootPath, "PrivateUploads", f.StoredFileName);
        if (!System.IO.File.Exists(path)) return NotFound();

        return PhysicalFile(path, f.ContentType, f.OriginalFileName);
    }

    [Authorize]
    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var q = await _db.Questionnaires.FindAsync(id);
        if (q is null) return NotFound();

        var lead = await _db.ContactLeads.FindAsync(q.ContactLeadId);
        if (lead is not null)
        {
            _db.ContactLeads.Remove(lead);
            await _db.SaveChangesAsync();
        }

        return Redirect("/admin");
    }

    [Authorize]
    [HttpPost("{id:int}/offer-sent")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OfferSent(int id)
    {
        var q = await _db.Questionnaires
            .Include(x => x.ContactLead)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (q is null) return NotFound();

        q.ContactLead.OfferSentAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Redirect($"/admin/{id}");
    }

    [Authorize]
    [HttpGet("documents")]
    public async Task<IActionResult> Documents()
    {
        var uploadsPath = PrivateUploadsPath();
        var docs = await _db.Documents
            .OrderByDescending(d => d.UploadedAt)
            .ToListAsync();

        var vms = docs.Select(d => new DocumentListItem
        {
            Id = d.Id,
            FullName = d.FullName,
            OriginalFileName = d.OriginalFileName,
            FilePath = Path.Combine(uploadsPath, d.StoredFileName),
            ContentType = d.ContentType,
            SizeBytes = d.SizeBytes,
            UploadedAt = d.UploadedAt
        }).ToList();

        return View(vms);
    }

    [Authorize]
    [HttpPost("documents/upload")]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(MaxFileSizeBytes + 1024)]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSizeBytes + 1024)]
    public async Task<IActionResult> DocumentUpload([FromForm] string fullName, IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            TempData["UploadError"] = "Nije odabran fajl.";
            return Redirect("/admin/documents");
        }

        if (file.Length > MaxFileSizeBytes)
        {
            TempData["UploadError"] = "Fajl je prevelik. Maksimalna veličina je 10 MB.";
            return Redirect("/admin/documents");
        }

        var ext = Path.GetExtension(file.FileName);
        if (!AllowedExtensions.Contains(ext))
        {
            TempData["UploadError"] = $"Tip fajla '{ext}' nije dozvoljen.";
            return Redirect("/admin/documents");
        }

        if (string.IsNullOrWhiteSpace(fullName))
            fullName = Path.GetFileNameWithoutExtension(file.FileName);

        var storedName = $"{Guid.NewGuid():N}{ext}";
        var uploadsPath = PrivateUploadsPath();
        Directory.CreateDirectory(uploadsPath);
        var destPath = Path.Combine(uploadsPath, storedName);

        await using (var stream = System.IO.File.Create(destPath))
            await file.CopyToAsync(stream);

        _db.Documents.Add(new Document
        {
            FullName = fullName.Trim(),
            OriginalFileName = file.FileName,
            StoredFileName = storedName,
            ContentType = file.ContentType ?? "application/octet-stream",
            SizeBytes = file.Length,
            UploadedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        return Redirect("/admin/documents");
    }

    [Authorize]
    [HttpGet("documents/{id:int}/download")]
    public async Task<IActionResult> DocumentDownload(int id)
    {
        var doc = await _db.Documents.FindAsync(id);
        if (doc is null) return NotFound();

        var path = Path.Combine(PrivateUploadsPath(), doc.StoredFileName);
        if (!System.IO.File.Exists(path)) return NotFound();

        return PhysicalFile(path, doc.ContentType, doc.OriginalFileName);
    }

    [Authorize]
    [HttpPost("documents/{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DocumentDelete(int id)
    {
        var doc = await _db.Documents.FindAsync(id);
        if (doc is null) return NotFound();

        var path = Path.Combine(PrivateUploadsPath(), doc.StoredFileName);
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        _db.Documents.Remove(doc);
        await _db.SaveChangesAsync();

        return Redirect("/admin/documents");
    }

    private string PrivateUploadsPath() =>
        Path.Combine(_env.ContentRootPath, "PrivateUploads");

    private static List<AnswerPair> ParseAnswers(string? json)
    {
        var result = new List<AnswerPair>();
        if (string.IsNullOrWhiteSpace(json)) return result;

        try
        {
            using var doc = JsonDocument.Parse(json);
            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                string value;
                if (prop.Value.ValueKind == JsonValueKind.Array)
                    value = string.Join(", ", prop.Value.EnumerateArray().Select(x => x.GetString() ?? ""));
                else
                    value = prop.Value.GetString() ?? "";

                if (!string.IsNullOrWhiteSpace(value))
                    result.Add(new AnswerPair { Label = QuestionLabels.GetLabel(prop.Name), Value = value });
            }
        }
        catch { }

        return result;
    }

    private static string Sha256(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}

public class QuestionnaireListItem
{
    public int Id { get; set; }
    public int ContactLeadId { get; set; }
    public string ClientName { get; set; } = "";
    public string ClientEmail { get; set; } = "";
    public byte Stage { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool OptedOut { get; set; }
    public DateTime? OfferSentAt { get; set; }
    public bool CompletionNotified { get; set; }
}

public class QuestionnaireDetailVm
{
    public int Id { get; set; }
    public int ContactLeadId { get; set; }
    public string ClientName { get; set; } = "";
    public string ClientEmail { get; set; } = "";
    public string Token { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public byte Stage { get; set; }
    public bool OptedOut { get; set; }
    public DateTime? OfferSentAt { get; set; }
    public List<AnswerPair> Step1 { get; set; } = [];
    public List<AnswerPair> Step2 { get; set; } = [];
    public List<AnswerPair> Step3 { get; set; } = [];
    public List<QuestionnaireFileItem> Files { get; set; } = [];
}

public class QuestionnaireFileItem
{
    public int Id { get; set; }
    public string FileLabel { get; set; } = "";
    public string OriginalFileName { get; set; } = "";
    public long SizeBytes { get; set; }
    public DateTime UploadedAt { get; set; }
}

public class AnswerPair
{
    public string Label { get; set; } = "";
    public string Value { get; set; } = "";
}

public class DocumentListItem
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string OriginalFileName { get; set; } = "";
    public string FilePath { get; set; } = "";
    public string ContentType { get; set; } = "";
    public long SizeBytes { get; set; }
    public DateTime UploadedAt { get; set; }
}
