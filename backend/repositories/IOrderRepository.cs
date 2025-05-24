using System.Collections.Generic;
using System.Threading.Tasks;
using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByCompanyIdAsync(int companyProfileId);
        Task<IEnumerable<Order>> GetOrdersByFreelancerIdAsync(int freelancerProfileId);
        Task<IEnumerable<Order>> GetOrdersByMentorIdAsync(int mentorProfileId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<Order>> GetOrdersByMentorWithDetailsAsync(int mentorProfileId);
    }
}