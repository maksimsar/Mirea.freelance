using Mirea.freelance.backend.models;
namespace Mirea.freelance.backend.dto;

public class CreateOrderDto
{
   public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public int CompanyProfileId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Open;
        public DateTime? Deadline { get; set; }
        public string RequiredRoles { get; set; } = string.Empty;
        public string PreferredContactMethods { get; set; } = string.Empty;
}