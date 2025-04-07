namespace Mirea.freelance.backend.dto;

public class CreateFeedbackDto
{
    public int OrderId { get; set; }
    public int AuthorProfileId { get; set; }
    public int RecipientProfileId { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}