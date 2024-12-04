public class Feedback
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int AuthorProfileId { get; set; }
    public int RecipientProfileId { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}