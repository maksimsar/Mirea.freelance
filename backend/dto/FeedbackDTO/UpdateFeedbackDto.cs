namespace Mirea.freelance.backend.dto;

public class UpdateFeedbackDto
{
    public decimal NewRating { get; set; }
    public string NewComment { get; set; } = string.Empty;
}