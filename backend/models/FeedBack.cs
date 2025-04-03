
namespace Mirea.freelance.backend.models;

public class Feedback
{
    public int Id { get; set; }
    
    // Если этот идентификатор относится к заказу, лучше назвать его OrderId для ясности:
    public int OrderId { get; set; }
    
    // Идентификаторы профилей, оставивших и получивших отзыв
    public int AuthorProfileId { get; set; }
    public int RecipientProfileId { get; set; }
    
    public decimal Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    // Навигационные свойства (добавьте их, если их ещё нет)
    public Order Order { get; set; } = null!;
    public Profile AuthorProfile { get; set; } = null!;
    public Profile RecipientProfile { get; set; } = null!;
}
