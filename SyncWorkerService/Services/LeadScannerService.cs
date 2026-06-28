using Microsoft.EntityFrameworkCore;
using SyncWorkerService.Data;

namespace SyncWorkerService.Services;

public class LeadScannerService(
    SyncDbContext db,
    EmailService emailService,
    IConfiguration config,
    ILogger<LeadScannerService> logger)
{
    public async Task ScanAndProcessAsync(CancellationToken ct)
    {
        var baseUrl = config["SyncWorker:QuestionnaireBaseUrl"]!.TrimEnd('/');
        var ownerEmail = config["Notification:OwnerEmail"] ?? "";

        await SendConfirmationEmailsAsync(baseUrl, ct);
        await SendQuestionnaireEmailsAsync(baseUrl, ct);
        await SendCompletionNotificationsAsync(ownerEmail, ct);
        await SendOptOutNotificationsAsync(ownerEmail, ct);
        await SendRegistrationNotificationsAsync(ownerEmail, ct);
    }

    private async Task SendConfirmationEmailsAsync(string baseUrl, CancellationToken ct)
    {
        var leads = await db.ContactLeads
            .Where(l => l.ConfirmationToken != null && l.ConfirmationEmailSentAt == null)
            .OrderBy(l => l.Id)
            .ToListAsync(ct);

        logger.LogInformation("Found {Count} lead(s) awaiting confirmation email", leads.Count);

        foreach (var lead in leads)
        {
            if (ct.IsCancellationRequested) break;
            var confirmUrl = $"{baseUrl}/confirm?token={lead.ConfirmationToken}";
            try
            {
                await emailService.SendConfirmationEmailAsync(lead.Email, lead.Name, confirmUrl, lead.Language);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send confirmation email to {Email} (lead {LeadId})", lead.Email, lead.Id);
                continue;
            }

            lead.ConfirmationEmailSentAt = DateTime.UtcNow;
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Confirmation email sent to {Email} (lead {LeadId})", lead.Email, lead.Id);
        }
    }

    private async Task SendQuestionnaireEmailsAsync(string baseUrl, CancellationToken ct)
    {
        var leads = await db.ContactLeads
            .Where(l => l.EmailConfirmedAt.HasValue && l.QuestionnaireEmailSentAt == null)
            .OrderBy(l => l.Id)
            .ToListAsync(ct);

        logger.LogInformation("Found {Count} confirmed lead(s) awaiting questionnaire email", leads.Count);

        foreach (var lead in leads)
        {
            if (ct.IsCancellationRequested) break;

            var token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
            var questionnaire = new Models.Questionnaire
            {
                ContactLeadId = lead.Id,
                Token = token,
                TokenExpiresAt = DateTime.UtcNow.AddDays(30),
                CreatedAt = DateTime.UtcNow
            };
            db.Questionnaires.Add(questionnaire);

            try { await db.SaveChangesAsync(ct); }
            catch (Exception ex) { logger.LogError(ex, "Failed to create questionnaire for lead {LeadId}", lead.Id); continue; }

            var url = $"{baseUrl}/questionnaire?token={token}";
            try { await emailService.SendQuestionnaireEmailAsync(lead.Email, lead.Name, url, lead.Language); }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send questionnaire email to {Email} (lead {LeadId})", lead.Email, lead.Id);
                db.Questionnaires.Remove(questionnaire);
                await db.SaveChangesAsync(ct);
                continue;
            }

            lead.QuestionnaireEmailSentAt = DateTime.UtcNow;
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Questionnaire email sent for lead {LeadId}", lead.Id);
        }
    }

    private async Task SendCompletionNotificationsAsync(string ownerEmail, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(ownerEmail)) return;

        var completed = await db.Questionnaires
            .Include(q => q.ContactLead)
            .Where(q => q.CompletedAt.HasValue && q.CompletionNotificationSentAt == null)
            .ToListAsync(ct);

        foreach (var q in completed)
        {
            if (ct.IsCancellationRequested) break;
            try { await emailService.SendCompletionNotificationAsync(ownerEmail, q.ContactLeadId, q.ContactLead.Email, q.ContactLead.Name); }
            catch (Exception ex) { logger.LogError(ex, "Failed to send completion notification for lead {LeadId}", q.ContactLeadId); continue; }

            q.CompletionNotificationSentAt = DateTime.UtcNow;
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Completion notification sent for lead {LeadId}", q.ContactLeadId);
        }
    }

    private async Task SendOptOutNotificationsAsync(string ownerEmail, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(ownerEmail)) return;

        var optedOut = await db.ContactLeads
            .Where(l => l.OptedOut && l.OptOutNotificationSentAt == null)
            .ToListAsync(ct);

        foreach (var lead in optedOut)
        {
            if (ct.IsCancellationRequested) break;
            try { await emailService.SendOptOutNotificationAsync(ownerEmail, lead.Name, lead.Email); }
            catch (Exception ex) { logger.LogError(ex, "Failed to send opt-out notification for lead {LeadId}", lead.Id); continue; }

            lead.OptOutNotificationSentAt = DateTime.UtcNow;
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Opt-out notification sent for lead {LeadId}", lead.Id);
        }
    }

    private async Task SendRegistrationNotificationsAsync(string ownerEmail, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(ownerEmail)) return;

        var newLeads = await db.ContactLeads
            .Where(l => l.ConfirmationToken != null && l.RegistrationNotificationSentAt == null)
            .ToListAsync(ct);

        foreach (var lead in newLeads)
        {
            if (ct.IsCancellationRequested) break;
            try { await emailService.SendRegistrationNotificationAsync(ownerEmail, lead.Name, lead.Email); }
            catch (Exception ex) { logger.LogError(ex, "Failed to send registration notification for lead {LeadId}", lead.Id); continue; }

            lead.RegistrationNotificationSentAt = DateTime.UtcNow;
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Registration notification sent for lead {LeadId}", lead.Id);
        }
    }
}
