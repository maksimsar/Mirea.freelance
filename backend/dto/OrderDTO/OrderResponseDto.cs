using Mirea.freelance.backend.models;
namespace Mirea.freelance.backend.dto;

public class OrderResponseDto
{
   public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public OrderStatus Status { get; set; } = OrderStatus.Open;
        public decimal Budget { get; set; }
        public int CompanyProfileId { get; set; }
        public CompanyProfileResponseDto CompanyProfile { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? Deadline { get; set; }
        public string RequiredRoles { get; set; } = string.Empty;
        public string PreferredContactMethods { get; set; } = string.Empty;
        public int? MentorProfileId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
}

