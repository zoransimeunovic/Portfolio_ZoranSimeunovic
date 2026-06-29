namespace Portfolio_ZoranSimeunovic.Models;

public class Questionnaire
{
    public int Id { get; set; }
    public int ContactLeadId { get; set; }
    public ContactLead ContactLead { get; set; } = null!;

    public string Token { get; set; } = string.Empty;
    public DateTime TokenExpiresAt { get; set; }

    public byte Stage { get; set; } = 0;

    public string? Step1Answers { get; set; }
    public string? Step2Answers { get; set; }
    public string? Step3Answers { get; set; }
    public string? Step4Answers { get; set; }
    public string? Step5Answers { get; set; }

    public bool PriceWarningAccepted { get; set; } = false;

    public DateTime? CompletedAt { get; set; }
    public DateTime? CompletionNotificationSentAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
