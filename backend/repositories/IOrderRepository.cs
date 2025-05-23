using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories;

public interface IOrderRepository
{
    //Получить заказ по его ID
    Task<Order?> GetByIdAsync(int id);
    
    //
    Task<IEnumerable<Order>> GetAllAsync();
    
    //
    Task AddAsync(Order order);
    
    //
    Task UpdateAsync(Order order);
    
    //
    Task DeleteAsync(int id);
    
    //
    Task<IEnumerable<Order>> GetOrdersByCompanyIdAsync(int companyProfileId);
    
    //
    Task<IEnumerable<Order>> GetOrdersByFreelancerIdAsync(int freelancerProfileId);
    
    //
    Task<IEnumerable<Order>> GetOrdersAsync();

    Task<IEnumerable<Order>> GetOrdersByMentorIdAsync(int mentorProfileId);

}