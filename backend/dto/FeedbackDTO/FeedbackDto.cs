namespace Mirea.freelance.backend.dto;

public class FeedbackDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int AuthorProfileId { get; set; }
    public int RecipientProfileId { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}