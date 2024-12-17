using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  
using System.Text.Json.Serialization;

public class Task
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("status")]
    public string Status { get; set; } = "Open";

    [Column("budget")]
    public decimal Budget { get; set; }

    [Column("clientprofileid")]
    [ForeignKey("ClientProfile")]
    public int ClientProfileId { get; set; }

    public Profile ClientProfile { get; set; }

    [Column("freelancerprofileid")]
    [ForeignKey("FreelancerProfile")]
    public int? FreelancerProfileId { get; set; }

    public Profile FreelancerProfile { get; set; }

    [Column("createddate")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Column("deadline")]
    public DateTime? Deadline { get; set; }
}
