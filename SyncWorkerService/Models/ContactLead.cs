namespace SyncWorkerService.Models;

public class ContactLead
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Language { get; set; } = "en";
    public DateTime CreatedAt { get; set; }
    public bool OptedOut { get; set; }
    public DateTime? OfferSentAt { get; set; }
}
