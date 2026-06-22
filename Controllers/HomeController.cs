using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Content;
using Portfolio_ZoranSimeunovic.Data;
using Portfolio_ZoranSimeunovic.Models;

namespace Portfolio_ZoranSimeunovic.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;
    private readonly MsGraphClient.MsGraphClient? _graph;
    private readonly IConfiguration _config;

    public HomeController(ILogger<HomeController> logger, AppDbContext db, IConfiguration config, MsGraphClient.MsGraphClient? graph = null)
    {
        _logger = logger;
        _db = db;
        _graph = graph;
        _config = config;
    }

    private SiteText CurrentText() =>
        SiteTextProvider.Get(CultureInfo.CurrentUICulture.Name);

    public IActionResult Index()
    {
        return View(CurrentText());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(string name, string email)
    {
        var text = CurrentText();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            return Json(new { success = false, message = text.Contact.ErrorMessage });

        var lead = new ContactLead
        {
            Name = name.Trim(),
            Email = email.Trim(),
            Language = CultureInfo.CurrentUICulture.Name,
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            _db.ContactLeads.Add(lead);
            await _db.SaveChangesAsync();
            _ = SendNewContactNotificationAsync(lead);
            return Json(new { success = true, message = text.Contact.SuccessMessage, leadId = lead.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Neuspjesno snimanje kontakta u bazu.");
            return Json(new { success = false, message = text.Contact.ErrorMessage });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateChecklist(int leadId, string? checklistJson = null)
    {
        try
        {
            var existing = await _db.ChecklistAnswers
                .Where(a => a.ContactLeadId == leadId)
                .ToListAsync();
            _db.ChecklistAnswers.RemoveRange(existing);

            if (!string.IsNullOrWhiteSpace(checklistJson))
            {
                var items = JsonSerializer.Deserialize<List<ChecklistItem>>(checklistJson);
                if (items != null)
                {
                    var now = DateTime.UtcNow;
                    foreach (var item in items)
                    {
                        if (string.IsNullOrWhiteSpace(item.Key) || string.IsNullOrWhiteSpace(item.Item))
                            continue;
                        _db.ChecklistAnswers.Add(new ChecklistAnswer
                        {
                            ContactLeadId = leadId,
                            ListKey = item.Key.Trim()[..Math.Min(item.Key.Trim().Length, 50)],
                            ItemText = item.Item.Trim()[..Math.Min(item.Item.Trim().Length, 500)],
                            CreatedAt = now
                        });
                    }
                }
            }

            await _db.SaveChangesAsync();

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Neuspjesno azuriranje checkliste za lead {LeadId}.", leadId);
            return Json(new { success = false });
        }
    }

    private async Task SendNewContactNotificationAsync(ContactLead lead)
    {
        if (_graph is null) return;
        try
        {
            var ownerEmail = _config["Notification:OwnerEmail"] ?? "";
            if (string.IsNullOrWhiteSpace(ownerEmail)) return;

            var body = $"""
                Novi kontakt na portfoliju!

                Ime:    {lead.Name}
                Email:  {lead.Email}
                Jezik:  {lead.Language}
                Datum:  {lead.CreatedAt:dd.MM.yyyy HH:mm} UTC

                Link za upitnik će biti automatski poslan u roku od 90 minuta.
                """;

            var result = await _graph.SendEmailAsync(ownerEmail, "Zoran", $"[Portfolio] Novi kontakt — {lead.Name}", body);
            if (!result.Success)
                _logger.LogWarning("Notifikacija novog kontakta nije poslana za {Email}: {Error}", lead.Email, result.Error);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Greška pri slanju notifikacije novog kontakta za {Email}", lead.Email);
        }
    }

    private class ChecklistItem
    {
        public string Key { get; set; } = string.Empty;
        public string Item { get; set; } = string.Empty;
    }

    [HttpGet]
    public IActionResult SetLanguage(string culture, string? returnUrl = null)
    {
        var allowed = new[] { "en", "de", "sr-Latn" };
        if (!allowed.Contains(culture))
            culture = "en";

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                Path = "/"
            });

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy() => View(CurrentText());

    public IActionResult Terms() => View(CurrentText());

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
