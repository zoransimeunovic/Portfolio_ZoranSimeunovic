using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SyncWorkerService.Services;

public class EmailService(IConfiguration config, ILogger<EmailService> logger)
{
    private const string SenderEmail = "noreply@zoransimeunovic.de";
    private const string SenderName = "Zoran Simeunović";

    public Task SendConfirmationEmailAsync(string toEmail, string toName, string confirmUrl, string language)
    {
        var (salutation, intro, please, button, after, expiry, ignore) = language switch
        {
            "de" => ("Guten Tag", "ich habe Ihre Anfrage über mein Portfolio erhalten.",
                     "Bitte bestätigen Sie Ihre E-Mail-Adresse:", "Anmeldung bestätigen",
                     "Nach der Bestätigung sende ich Ihnen einen Fragebogen, der mir hilft, Ihre Bedürfnisse zu verstehen.",
                     "Der Link ist 48 Stunden gültig.",
                     "Falls Sie sich nicht angemeldet haben, ignorieren Sie diese E-Mail bitte."),
            "sr-Latn" => ("Poštovani", "primio sam Vašu prijavu putem mog portfolija.",
                          "Molim Vas da potvrdite Vašu email adresu:", "Potvrdite prijavu",
                          "Nakon potvrde, poslat ću Vam upitnik koji će mi pomoći da razumijem Vaše potrebe.",
                          "Link je aktivan 48 sati.",
                          "Ako se niste prijavili, jednostavno zanemarite ovaj email."),
            _ => ("Dear", "I received your enquiry through my portfolio.",
                  "Please confirm your email address:", "Confirm registration",
                  "After confirmation, I'll send you a questionnaire to help me understand your needs.",
                  "The link is valid for 48 hours.",
                  "If you didn't register, simply ignore this email.")
        };

        var subject = language switch
        {
            "de" => "Bestätigen Sie Ihre Anmeldung | Zoran Simeunović",
            "sr-Latn" => "Potvrdite Vašu prijavu | Zoran Simeunović",
            _ => "Confirm your registration | Zoran Simeunović"
        };

        var text = $"{salutation} {toName},\n\n{intro}\n\n{please}\n{confirmUrl}\n\n{after}\n\n{expiry} {ignore}\n\nZoran Simeunović\nFull Stack Web Developer\nzoransimeunovic.de";
        var html = $"<div style=\"font-family:Arial,sans-serif;color:#333333;font-size:15px;line-height:1.6;padding:24px\"><p style=\"margin:0 0 12px\">{salutation} {toName},</p><p style=\"margin:0 0 12px\">{intro}</p><p style=\"margin:0 0 12px\">{please}</p><p style=\"margin:24px 0\"><a href=\"{confirmUrl}\" style=\"background:#156EF6;color:#ffffff;padding:14px 28px;text-decoration:none;border-radius:6px;font-weight:bold;display:inline-block;font-family:Arial,sans-serif\">{button}</a></p><p style=\"margin:0 0 12px\">{after}</p><p style=\"margin:0;color:#888888;font-size:13px\">{expiry} {ignore}</p><hr style=\"border:none;border-top:1px solid #eeeeee;margin:24px 0\"><p style=\"margin:0;color:#aaaaaa;font-size:12px\">Zoran Simeunović &middot; Full Stack Web Developer &middot; zoransimeunovic.de</p></div>";

        return SendAsync(toEmail, toName, subject, text, html);
    }

    public Task SendQuestionnaireEmailAsync(string toEmail, string toName, string questionnaireUrl, string language)
    {
        var (salutation, thanks, intro, button, expiry) = language switch
        {
            "de" => ("Guten Tag", "vielen Dank für die Bestätigung Ihrer E-Mail-Adresse!",
                     "Um Ihnen das bestmögliche Angebot zu erstellen, habe ich einen Fragebogen vorbereitet, der mir hilft, Ihre Bedürfnisse und Ziele besser zu verstehen.",
                     "Fragebogen öffnen", "Der Link ist 30 Tage gültig."),
            "sr-Latn" => ("Poštovani", "hvala što ste potvrdili svoju email adresu!",
                          "Kako bih Vam mogao pripremiti što bolji prijedlog i ponudu, napravio sam upitnik koji će mi pomoći da bolje razumijem Vaše potrebe i ciljeve.",
                          "Otvori upitnik", "Link je aktivan 30 dana."),
            _ => ("Dear", "thank you for confirming your email address!",
                  "To prepare the best possible proposal for you, I've created a questionnaire to help me better understand your needs and goals.",
                  "Open questionnaire", "The link is valid for 30 days.")
        };

        var subject = language switch
        {
            "de" => "Fragebogen — Analyse Ihrer Bedürfnisse | Zoran Simeunović",
            "sr-Latn" => "Upitnik — Analiza Vaših potreba | Zoran Simeunović",
            _ => "Questionnaire — Analysis of your needs | Zoran Simeunović"
        };

        var text = $"{salutation} {toName},\n\n{thanks}\n\n{intro}\n\n{questionnaireUrl}\n\n{expiry}\n\nZoran Simeunović\nFull Stack Web Developer\nzoransimeunovic.de";
        var html = $"<div style=\"font-family:Arial,sans-serif;color:#333333;font-size:15px;line-height:1.6;padding:24px\"><p style=\"margin:0 0 12px\">{salutation} {toName},</p><p style=\"margin:0 0 12px\">{thanks}</p><p style=\"margin:0 0 12px\">{intro}</p><p style=\"margin:24px 0\"><a href=\"{questionnaireUrl}\" style=\"background:#156EF6;color:#ffffff;padding:14px 28px;text-decoration:none;border-radius:6px;font-weight:bold;display:inline-block;font-family:Arial,sans-serif\">{button}</a></p><p style=\"margin:0;color:#888888;font-size:13px\">{expiry}</p><hr style=\"border:none;border-top:1px solid #eeeeee;margin:24px 0\"><p style=\"margin:0;color:#aaaaaa;font-size:12px\">Zoran Simeunović &middot; Full Stack Web Developer &middot; zoransimeunovic.de</p></div>";

        return SendAsync(toEmail, toName, subject, text, html);
    }

    public Task SendRegistrationNotificationAsync(string ownerEmail, string leadName, string leadEmail) =>
        SendAsync(ownerEmail, "Zoran",
            $"[Portfolio] Novi kontakt — {leadName}",
            $"""
            Novi posjetilac se prijavio!

            Ime:   {leadName}
            Email: {leadEmail}
            Datum: {DateTime.UtcNow:dd.MM.yyyy HH:mm} UTC

            Čeka potvrdu email adrese.
            """);

    public Task SendCompletionNotificationAsync(string ownerEmail, int leadId, string leadEmail, string leadName)
    {
        var baseUrl = config["SyncWorker:QuestionnaireBaseUrl"]!.TrimEnd('/');
        var adminUrl = $"{baseUrl}/admin/detail/{leadId}";
        return SendAsync(ownerEmail, "Zoran",
            $"[Portfolio] Upitnik popunjen — {leadName}",
            $"Upitnik završen!\n\nLeadId: {leadId}\nIme:    {leadName}\nEmail:  {leadEmail}\n\nProvjeri detalje u admin panelu:\n{adminUrl}");
    }

    public Task SendOptOutNotificationAsync(string ownerEmail, string leadName, string leadEmail) =>
        SendAsync(ownerEmail, "Zoran",
            $"[Portfolio] Odjava — {leadName}",
            $"""
            Klijent se odjavio!

            Ime:   {leadName}
            Email: {leadEmail}
            Datum: {DateTime.UtcNow:dd.MM.yyyy HH:mm} UTC
            """);

    private async Task SendAsync(string toEmail, string toName, string subject, string textBody, string? htmlBody = null)
    {
        var apiKey = config["Brevo:ApiKey"]
            ?? throw new InvalidOperationException("Brevo:ApiKey nije konfigurisan.");

        var payload = htmlBody is not null
            ? (object)new
            {
                sender = new { name = SenderName, email = SenderEmail },
                to = new[] { new { email = toEmail, name = toName } },
                subject,
                htmlContent = htmlBody,
                textContent = textBody
            }
            : new
            {
                sender = new { name = SenderName, email = SenderEmail },
                to = new[] { new { email = toEmail, name = toName } },
                subject,
                textContent = textBody
            };

        var json = JsonSerializer.Serialize(payload);

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("api-key", apiKey);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        logger.LogInformation("Sending email to {Email} — {Subject}", toEmail, subject);

        var response = await client.PostAsync(
            "https://api.brevo.com/v3/smtp/email",
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Email successfully sent to {Email}", toEmail);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            logger.LogError("Brevo rejected email to {Email}: {Status} — {Error}", toEmail, response.StatusCode, error);
            throw new InvalidOperationException($"Brevo error {response.StatusCode}: {error}");
        }
    }
}
