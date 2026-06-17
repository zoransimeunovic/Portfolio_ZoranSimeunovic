using Microsoft.AspNetCore.Localization;

namespace Portfolio_ZoranSimeunovic.Localization;

/// <summary>
/// Bira jezik na osnovu Accept-Language headera browsera.
/// Mapiranje:
///   de*                              -> de
///   sr, hr, bs, cnr, mk, sl ...      -> sr-Latn  ( exYu jezici)
///   en*                              -> en
///   sve ostalo                       -> en (fallback)
/// Cookie (rucni izbor korisnika) ima prioritet jer se CookieRequestCultureProvider
/// registruje prije ovog providera.
/// </summary>
public class BrowserCultureProvider : RequestCultureProvider
{
    // exYu jezicki kodovi koji se mapiraju na srpski-latinica
    private static readonly string[] ExYuLanguages =
        { "sr", "hr", "bs", "cnr", "me", "mk", "sh" };

    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var acceptLanguage = httpContext.Request.Headers.AcceptLanguage.ToString();

        if (string.IsNullOrWhiteSpace(acceptLanguage))
            return NullResult;

        // Uzmi prvi (najprioritetniji) jezik iz liste, npr. "de-DE,de;q=0.9,en;q=0.8"
        foreach (var part in acceptLanguage.Split(','))
        {
            var lang = part.Split(';')[0].Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(lang))
                continue;

            var primary = lang.Split('-')[0];

            if (primary == "de")
                return Result("de");

            if (ExYuLanguages.Contains(primary))
                return Result("sr-Latn");

            if (primary == "en")
                return Result("en");
        }

        return NullResult;
    }

    private static Task<ProviderCultureResult?> Result(string culture) =>
        Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(culture, culture));

    private static Task<ProviderCultureResult?> NullResult =>
        Task.FromResult<ProviderCultureResult?>(null);
}
