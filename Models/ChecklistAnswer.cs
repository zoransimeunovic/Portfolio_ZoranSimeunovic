using System.ComponentModel.DataAnnotations;

namespace Portfolio_ZoranSimeunovic.Models;

public class ChecklistAnswer
{
    public int Id { get; set; }
    public int ContactLeadId { get; set; }
    public ContactLead ContactLead { get; set; } = null!;

    [Required, StringLength(50)]
    public string ListKey { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string ItemText { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
