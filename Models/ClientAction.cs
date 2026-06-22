namespace Portfolio_ZoranSimeunovic.Models;

public class ClientAction
{
    public int Id { get; set; }
    public int ContactLeadId { get; set; }
    public ContactLead ContactLead { get; set; } = null!;

    public string ActionType { get; set; } = string.Empty;
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
}
