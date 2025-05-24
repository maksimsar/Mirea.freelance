using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.dto;


public class UpdateOrderDto
{
   public string NewTitle { get; set; } = string.Empty;
        public string NewDescription { get; set; } = string.Empty;
        public decimal NewBudget { get; set; }
        public DateTime? NewDeadline { get; set; }
        public string NewRequiredRoles { get; set; } = string.Empty;
}