using Microsoft.AspNetCore.Localization;

namespace Portfolio_ZoranSimeunovic.Localization;

// de* → de; sr/hr/bs/cnr/me/mk/sh → sr-Latn; en* → en; sve ostalo → en
// Cookie ima prioritet — registruje se prije ovog providera
public class BrowserCultureProvider : RequestCultureProvider
{
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
