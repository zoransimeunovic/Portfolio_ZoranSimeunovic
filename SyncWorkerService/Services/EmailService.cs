namespace SyncWorkerService.Services;

public class EmailService(MsGraphClient.MsGraphClient graph, ILogger<EmailService> logger)
{
    public async Task SendQuestionnaireEmailAsync(string toEmail, string toName, string questionnaireUrl)
    {
        var body = $"""
            Poštovani {toName},

            hvala što ste me kontaktirali!

            Kako bih mogao da Vam pripremim što bolji prijedlog, pripremio sam kratki upitnik koji će mi pomoći da bolje razumijem Vaše potrebe i ciljeve.

            Popunite upitnik ovdje:
            {questionnaireUrl}

            Link je aktivan 30 dana.

            Ako imate pitanja, slobodno me kontaktirajte na zoran.simeunovic@outlook.de.

            Srdačan pozdrav,
            Zoran Simeunović
            Full Stack Web Developer
            zoransimeunovic.de
            """;

        var result = await graph.SendEmailAsync(toEmail, toName, "Upitnik — Analiza Vaših potreba | Zoran Simeunović", body);

        if (result.Success)
            logger.LogInformation("Questionnaire email sent to {Email}", toEmail);
        else
        {
            logger.LogError("Failed to send questionnaire email to {Email}: {Error}", toEmail, result.Error ?? "unknown");
            throw new InvalidOperationException(result.Error);
        }
    }

    public async Task SendCompletionNotificationAsync(int leadId, string leadEmail, string leadName)
    {
        var body = $"""
            Novi upitnik je popunjen!

            LeadId: {leadId}
            Ime: {leadName}
            Email: {leadEmail}

            Provjeri unose u bazi ili portfolio admin panel.
            """;

        var result = await graph.SendEmailAsync(
            "zoran.simeunovic@outlook.de",
            "Zoran",
            $"[Portfolio] Upitnik popunjen — {leadName}",
            body);

        if (!result.Success)
            logger.LogError("Failed to send completion notification for lead {LeadId}: {Error}", leadId, result.Error);
    }
}
