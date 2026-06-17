using System.ComponentModel.DataAnnotations;

namespace Portfolio_ZoranSimeunovic.Models;

/// <summary>
/// Kontakt podaci novog klijenta iz "Take the first step" forme.
/// </summary>
public class ContactLead
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    /// <summary>Jezik sajta u trenutku slanja (en / de / sr-Latn).</summary>
    [StringLength(10)]
    public string? Language { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
