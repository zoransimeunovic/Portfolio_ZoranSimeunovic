using Microsoft.EntityFrameworkCore;
using SyncWorkerService.Data;
using SyncWorkerService.Models;

namespace SyncWorkerService.Services;

public class LeadScannerService(
    SyncDbContext db,
    EmailService emailService,
    IConfiguration config,
    ILogger<LeadScannerService> logger)
{
    private const string ActionQuestionnaireEmail = "questionnaire_email_sent";
    private const string ActionCompletionNotification = "completion_notification_sent";

    public async Task ScanAndProcessAsync(CancellationToken ct)
    {
        var baseUrl = config["SyncWorker:QuestionnaireBaseUrl"]!.TrimEnd('/');

        var sentLeadIds = await db.ClientActions
            .Where(a => a.ActionType == ActionQuestionnaireEmail)
            .Select(a => a.ContactLeadId)
            .ToListAsync(ct);

        var newLeads = await db.ContactLeads
            .Where(l => !sentLeadIds.Contains(l.Id))
            .OrderBy(l => l.Id)
            .ToListAsync(ct);

        logger.LogInformation("Found {Count} new lead(s) to process", newLeads.Count);

        foreach (var lead in newLeads)
        {
            if (ct.IsCancellationRequested) break;
            await ProcessLeadAsync(lead, baseUrl, ct);
        }

        await NotifyCompletedQuestionnairesAsync(ct);
    }

    private async Task NotifyCompletedQuestionnairesAsync(CancellationToken ct)
    {
        var notifiedLeadIds = await db.ClientActions
            .Where(a => a.ActionType == ActionCompletionNotification)
            .Select(a => a.ContactLeadId)
            .ToListAsync(ct);

        var completed = await db.Questionnaires
            .Include(q => q.ContactLead)
            .Where(q => q.CompletedAt.HasValue && !notifiedLeadIds.Contains(q.ContactLeadId))
            .ToListAsync(ct);

        logger.LogInformation("Found {Count} completed questionnaire(s) to notify", completed.Count);

        foreach (var q in completed)
        {
            if (ct.IsCancellationRequested) break;

            try
            {
                await emailService.SendCompletionNotificationAsync(q.ContactLeadId, q.ContactLead.Email, q.ContactLead.Name);
            }
            catch
            {
                continue;
            }

            db.ClientActions.Add(new ClientAction
            {
                ContactLeadId = q.ContactLeadId,
                ActionType = ActionCompletionNotification,
                ExecutedAt = DateTime.UtcNow
            });

            await db.SaveChangesAsync(ct);
            logger.LogInformation("Completion notification sent for lead {LeadId}", q.ContactLeadId);
        }
    }

    private async Task ProcessLeadAsync(ContactLead lead, string baseUrl, CancellationToken ct)
    {
        var token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        var expiresAt = DateTime.UtcNow.AddDays(30);

        var questionnaire = new Questionnaire
        {
            ContactLeadId = lead.Id,
            Token = token,
            TokenExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow
        };

        db.Questionnaires.Add(questionnaire);

        try
        {
            await db.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create questionnaire record for lead {LeadId}", lead.Id);
            return;
        }

        var url = $"{baseUrl}/questionnaire?token={token}";

        try
        {
            await emailService.SendQuestionnaireEmailAsync(lead.Email, lead.Name, url);
        }
        catch
        {
            db.Questionnaires.Remove(questionnaire);
            await db.SaveChangesAsync(ct);
            return;
        }

        db.ClientActions.Add(new ClientAction
        {
            ContactLeadId = lead.Id,
            ActionType = ActionQuestionnaireEmail,
            ExecutedAt = DateTime.UtcNow
        });

        await db.SaveChangesAsync(ct);
        logger.LogInformation("Processed lead {LeadId} ({Email})", lead.Id, lead.Email);
    }
}
