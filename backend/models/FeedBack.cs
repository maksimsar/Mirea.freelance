
namespace Mirea.freelance.backend.models;

public class Feedback
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int AuthorProfileId { get; set; }
    public int RecipientProfileId { get; set; }
    
    public decimal Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public Order Order { get; set; } = null!;
    public Profile AuthorProfile { get; set; } = null!;
    public Profile RecipientProfile { get; set; } = null!;
}
