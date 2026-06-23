using System.ComponentModel.DataAnnotations;

namespace Portfolio_ZoranSimeunovic.Models;

public class ContactLead
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [StringLength(10)]
    public string? Language { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool OptedOut { get; set; } = false;
    public DateTime? OfferSentAt { get; set; }
    public string? ConfirmationToken { get; set; }
    public DateTime? ConfirmationTokenExpiresAt { get; set; }
    public DateTime? EmailConfirmedAt { get; set; }
    public DateTime? ConfirmationEmailSentAt { get; set; }
    public DateTime? QuestionnaireEmailSentAt { get; set; }
    public DateTime? RegistrationNotificationSentAt { get; set; }
    public DateTime? OptOutNotificationSentAt { get; set; }
}
