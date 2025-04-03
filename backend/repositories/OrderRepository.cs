using Mirea.freelance.backend.models;
using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;

namespace Mirea.freelance.backend.repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    //
    public async Task<Order?> GetByIdAsync(int id)
    {
        //
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    //
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        //
        return await _context.Orders
            .ToListAsync();
    }

    //
    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    //
    public async Task UpdateAsync(Order order)
    {
        //
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    //
    public async Task DeleteAsync(int id)
    {
        //
        var orderToDelete = await GetByIdAsync(id);
        if (orderToDelete != null)
        {
            _context.Orders.Remove(orderToDelete);
            await _context.SaveChangesAsync();
        }
    }

    //
    public async Task<IEnumerable<Order>> GetOrdersByCompanyIdAsync(int companyProfileId)
    {
        // Выбираем все заказы, у которых CompanyProfileId совпадает с переданным параметром
        return await _context.Orders
            .Where(o => o.CompanyProfileId == companyProfileId)
            .ToListAsync();

    }

    //
    public async Task<IEnumerable<Order>> GetOrdersByFreelancerIdAsync(int freelancerProfileId)
    {
        // Находим все заказы, у которых среди FreelancerProfiles есть профиль с UserId = freelancerProfileId
        return await _context.Orders
            .Where(o => o.FreelancerProfiles.Any(fp => fp.UserId == freelancerProfileId))
            .ToListAsync();
        
    }
    
    //
    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        // Пример: вернуть только заказы со статусом "Open"
        return await _context.Orders
            .Where(o => o.Status == "Open")
            .ToListAsync();
        
    }

}