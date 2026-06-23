using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SyncWorkerService.Services;

public class EmailService(IConfiguration config, ILogger<EmailService> logger)
{
    private const string SenderEmail = "noreply@zoransimeunovic.de";
    private const string SenderName = "Zoran Simeunović";

    public Task SendConfirmationEmailAsync(string toEmail, string toName, string confirmUrl) =>
        SendAsync(toEmail, toName,
            "Potvrdite Vašu prijavu | Zoran Simeunović",
            $"Poštovani {toName},\n\nprimio sam Vašu prijavu putem mog portfolija.\n\nMolim Vas da potvrdite Vašu email adresu klikom na link ispod:\n{confirmUrl}\n\nNakon potvrde, poslat ću Vam kratki upitnik koji će mi pomoći da razumijem Vaše potrebe.\n\nLink je aktivan 48 sati. Ako se niste prijavili, jednostavno zanemarite ovaj email.\n\nSrdačan pozdrav,\nZoran Simeunović\nFull Stack Web Developer\nzoransimeunovic.de",
            "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#ffffff\"><tr><td align=\"center\" style=\"padding:40px 16px\"><table width=\"560\" cellpadding=\"0\" cellspacing=\"0\"><tr><td style=\"font-family:Arial,sans-serif;color:#333333;font-size:15px;line-height:1.6;padding:0 0 16px\"><p style=\"margin:0 0 12px\">Poštovani " + toName + ",</p><p style=\"margin:0 0 12px\">primio sam Vašu prijavu putem mog portfolija.</p><p style=\"margin:0 0 12px\">Molim Vas da potvrdite Vašu email adresu:</p><p style=\"margin:24px 0\"><a href=\"" + confirmUrl + "\" style=\"background:#156EF6;color:#ffffff;padding:14px 28px;text-decoration:none;border-radius:6px;font-weight:bold;display:inline-block;font-family:Arial,sans-serif\">Potvrdite prijavu</a></p><p style=\"margin:0 0 12px\">Nakon potvrde, poslat ću Vam kratki upitnik koji će mi pomoći da razumijem Vaše potrebe.</p><p style=\"margin:0;color:#888888;font-size:13px\">Link je aktivan 48 sati. Ako se niste prijavili, jednostavno zanemarite ovaj email.</p></td></tr><tr><td style=\"border-top:1px solid #eeeeee;padding:16px 0 0;font-family:Arial,sans-serif;color:#aaaaaa;font-size:12px\">Zoran Simeunović &middot; Full Stack Web Developer &middot; zoransimeunovic.de</td></tr></table></td></tr></table>");

    public Task SendQuestionnaireEmailAsync(string toEmail, string toName, string questionnaireUrl) =>
        SendAsync(toEmail, toName,
            "Upitnik — Analiza Vaših potreba | Zoran Simeunović",
            $"Poštovani {toName},\n\nhvala što ste potvrdili svoju email adresu!\n\nKako bih Vam mogao pripremiti što bolji prijedlog i ponudu, napravio sam kratki upitnik koji će mi pomoći da bolje razumijem Vaše potrebe i ciljeve.\n\nPopunite upitnik ovdje:\n{questionnaireUrl}\n\nLink je aktivan 30 dana.\n\nSrdačan pozdrav,\nZoran Simeunović\nFull Stack Web Developer\nzoransimeunovic.de",
            "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#ffffff\"><tr><td align=\"center\" style=\"padding:40px 16px\"><table width=\"560\" cellpadding=\"0\" cellspacing=\"0\"><tr><td style=\"font-family:Arial,sans-serif;color:#333333;font-size:15px;line-height:1.6;padding:0 0 16px\"><p style=\"margin:0 0 12px\">Poštovani " + toName + ",</p><p style=\"margin:0 0 12px\">hvala što ste potvrdili svoju email adresu!</p><p style=\"margin:0 0 12px\">Kako bih Vam mogao pripremiti što bolji prijedlog i ponudu, napravio sam kratki upitnik koji će mi pomoći da bolje razumijem Vaše potrebe i ciljeve.</p><p style=\"margin:24px 0\"><a href=\"" + questionnaireUrl + "\" style=\"background:#156EF6;color:#ffffff;padding:14px 28px;text-decoration:none;border-radius:6px;font-weight:bold;display:inline-block;font-family:Arial,sans-serif\">Otvori upitnik</a></p><p style=\"margin:0;color:#888888;font-size:13px\">Link je aktivan 30 dana.</p></td></tr><tr><td style=\"border-top:1px solid #eeeeee;padding:16px 0 0;font-family:Arial,sans-serif;color:#aaaaaa;font-size:12px\">Zoran Simeunović &middot; Full Stack Web Developer &middot; zoransimeunovic.de</td></tr></table></td></tr></table>");

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

    public Task SendCompletionNotificationAsync(string ownerEmail, int leadId, string leadEmail, string leadName) =>
        SendAsync(ownerEmail, "Zoran",
            $"[Portfolio] Upitnik popunjen — {leadName}",
            $"""
            Upitnik završen!

            LeadId: {leadId}
            Ime:    {leadName}
            Email:  {leadEmail}

            Provjeri detalje u admin panelu.
            """);

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
