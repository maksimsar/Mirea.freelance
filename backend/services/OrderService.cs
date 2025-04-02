using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Преобразуем deadline в UTC, если оно не null
            if (order.Deadline.HasValue)
            {
                order.Deadline = TimeZoneInfo.ConvertTimeToUtc(order.Deadline.Value);
            }

            // Устанавливаем текущую дату времени в UTC
            order.CreatedDate = DateTime.UtcNow;

            _context.Orders.Add(order); // Изменено: используем Orders вместо Tasks
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.ClientProfile)
                .Include(o => o.FreelancerProfile)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order updatedOrder)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null) return null;

            existingOrder.Title = updatedOrder.Title;
            existingOrder.Description = updatedOrder.Description;
            existingOrder.Status = updatedOrder.Status;
            existingOrder.Budget = updatedOrder.Budget;
            
            if (updatedOrder.Deadline.HasValue)
            {
                existingOrder.Deadline = updatedOrder.Deadline.Value.ToUniversalTime();
            }

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
