namespace Portfolio_ZoranSimeunovic.Models;

public class QuestionnaireFile
{
    public int Id { get; set; }
    public int QuestionnaireId { get; set; }
    public Questionnaire Questionnaire { get; set; } = null!;
    public string FileLabel { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
