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

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    private SiteText CurrentText() =>
        SiteTextProvider.Get(CultureInfo.CurrentUICulture.Name);

    public async Task<IActionResult> Index([FromQuery(Name = "ref")] string? refToken = null)
    {
        _logger.LogInformation("Index: GET / refToken={HasRef}", !string.IsNullOrWhiteSpace(refToken));
        try
        {
            if (!string.IsNullOrWhiteSpace(refToken))
            {
                var valid = await _db.Questionnaires.AnyAsync(
                    x => x.Token == refToken && x.TokenExpiresAt > DateTime.UtcNow && !x.CompletedAt.HasValue);

                _logger.LogInformation("Index: refToken valid={Valid}", valid);

                if (valid)
                {
                    Response.Cookies.Append("q_ref", refToken, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30),
                        IsEssential = true,
                        Path = "/",
                        SameSite = SameSiteMode.Lax
                    });
                }

                return LocalRedirect("/");
            }

            return View(CurrentText());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Index: neočekivana greška");
            throw;
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(string name, string email)
    {
        _logger.LogWarning("Contact: START name={Name} email={Email}", name, email);
        try
        {
            var text = CurrentText();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning("Contact: VALIDATION FAIL — prazno ime ili email");
                return Json(new { success = false, message = text.Contact.ErrorMessage });
            }

            var trimEmail = email.Trim();
            _logger.LogInformation("Contact: trazim postojeci lead za {Email}", trimEmail);
            var existing = await _db.ContactLeads.FirstOrDefaultAsync(l => l.Email == trimEmail);

            if (existing != null)
            {
                _logger.LogInformation("Contact: DUPLICATE — lead vec postoji id={Id}", existing.Id);
                await HandleDuplicateRegistrationAsync(existing);
                return Json(new { success = true, message = text.Contact.SuccessMessage });
            }

            var token = GenerateToken();
            _logger.LogInformation("Contact: NOVI LEAD — token generisan, duzina={Len}", token.Length);

            var lead = new ContactLead
            {
                Name = name.Trim(),
                Email = trimEmail,
                Language = CultureInfo.CurrentUICulture.Name,
                CreatedAt = DateTime.UtcNow,
                ConfirmationToken = token,
                ConfirmationTokenExpiresAt = DateTime.UtcNow.AddHours(48)
            };

            _logger.LogInformation("Contact: pozivam SaveChangesAsync, token={Token}", token[..8] + "...");
            _db.ContactLeads.Add(lead);
            await _db.SaveChangesAsync();
            _logger.LogWarning("Contact: SUCCESS — lead.Id={Id} token={Token}", lead.Id, token[..8] + "...");

            return Json(new { success = true, message = text.Contact.SuccessMessage });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Contact: GREŠKA");
            var text = CurrentText();
            return Json(new { success = false, message = text.Contact.ErrorMessage });
        }
    }

    [HttpGet("/confirm")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        _logger.LogInformation("ConfirmEmail: START token={HasToken}", !string.IsNullOrWhiteSpace(token));
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogWarning("ConfirmEmail: token prazan — redirect na Index");
                return RedirectToAction(nameof(Index));
            }

            var lead = await _db.ContactLeads.FirstOrDefaultAsync(l => l.ConfirmationToken == token);

            if (lead == null)
            {
                _logger.LogWarning("ConfirmEmail: lead nije pronađen za token={Token}", token[..8] + "...");
                return View("ConfirmExpired");
            }

            if (lead.ConfirmationTokenExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("ConfirmEmail: token istekao za lead.Id={Id}", lead.Id);
                return View("ConfirmExpired");
            }

            if (lead.EmailConfirmedAt.HasValue)
            {
                _logger.LogInformation("ConfirmEmail: već potvrđen lead.Id={Id}", lead.Id);
                return View("Confirmed");
            }

            lead.EmailConfirmedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            _logger.LogWarning("ConfirmEmail: SUCCESS — lead.Id={Id} potvrđen", lead.Id);

            return View("Confirmed");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConfirmEmail: GREŠKA");
            throw;
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateChecklist(int leadId, string? checklistJson = null)
    {
        _logger.LogInformation("UpdateChecklist: START leadId={LeadId}", leadId);
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
            _logger.LogInformation("UpdateChecklist: SUCCESS leadId={LeadId}", leadId);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateChecklist: GREŠKA leadId={LeadId}", leadId);
            return Json(new { success = false });
        }
    }

    private static string GenerateToken() =>
        Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");

    private async Task HandleDuplicateRegistrationAsync(ContactLead lead)
    {
        _logger.LogInformation("HandleDuplicate: START lead.Id={Id} emailConfirmed={Confirmed}", lead.Id, lead.EmailConfirmedAt.HasValue);
        try
        {
            if (!lead.EmailConfirmedAt.HasValue)
            {
                var newToken = GenerateToken();
                lead.ConfirmationToken = newToken;
                lead.ConfirmationTokenExpiresAt = DateTime.UtcNow.AddHours(48);
                lead.ConfirmationEmailSentAt = null;
                await _db.SaveChangesAsync();
                _logger.LogWarning("HandleDuplicate: novi token za nepotvrdjen lead.Id={Id} token={Token}", lead.Id, newToken[..8] + "...");
                return;
            }

            var q = await _db.Questionnaires.FirstOrDefaultAsync(x => x.ContactLeadId == lead.Id && !x.CompletedAt.HasValue);
            if (q != null)
            {
                q.TokenExpiresAt = DateTime.UtcNow.AddDays(30);
                await _db.SaveChangesAsync();
                _logger.LogInformation("HandleDuplicate: produžen token upitnika q.Id={Id}", q.Id);
            }
            else
            {
                _logger.LogInformation("HandleDuplicate: potvrđen lead bez aktivnog upitnika, ništa ne radim");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HandleDuplicate: GREŠKA za lead.Id={Id}", lead.Id);
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
        _logger.LogInformation("SetLanguage: culture={Culture}", culture);
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "SetLanguage: GREŠKA culture={Culture}", culture);
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Privacy() => View(CurrentText());

    public IActionResult Terms() => View(CurrentText());

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
