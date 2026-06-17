using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
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

    public IActionResult Index()
    {
        return View(CurrentText());
    }

    /// <summary>
    /// Snima kontakt podatke novog klijenta iz "Take the first step" forme.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(string name, string email)
    {
        var text = CurrentText();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        {
            return Json(new { success = false, message = text.Contact.ErrorMessage });
        }

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
            return Json(new { success = true, message = text.Contact.SuccessMessage });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Neuspjesno snimanje kontakta u bazu.");
            return Json(new { success = false, message = text.Contact.ErrorMessage });
        }
    }

    /// <summary>
    /// Rucna promjena jezika - cuva izbor u cookie i vraca na prethodnu stranicu.
    /// </summary>
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
