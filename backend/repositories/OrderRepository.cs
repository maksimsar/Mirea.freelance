using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirea.freelance.backend.repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.CompanyProfile)
                    .ThenInclude(cp => cp.Contacts)
                .Include(o => o.MentorProfile)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.CompanyProfile)
                    .ThenInclude(cp => cp.Contacts)
                .Include(o => o.MentorProfile)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByCompanyIdAsync(int companyProfileId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.CompanyProfileId == companyProfileId)
                .Include(o => o.CompanyProfile)
                    .ThenInclude(cp => cp.Contacts)
                .Include(o => o.MentorProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByFreelancerIdAsync(int freelancerProfileId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.ProjectStudents)
                    .ThenInclude(ps => ps.Student)
                .Include(o => o.CompanyProfile)
                    .ThenInclude(cp => cp.Contacts)
                .Include(o => o.MentorProfile)
                .Where(o => o.ProjectStudents.Any(ps => ps.StudentId == freelancerProfileId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByMentorIdAsync(int mentorProfileId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.MentorProfileId == mentorProfileId)
                .Include(o => o.CompanyProfile)
                    .ThenInclude(cp => cp.Contacts)
                .Include(o => o.MentorProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Status == status)
                .Include(o => o.CompanyProfile)
                    .ThenInclude(cp => cp.Contacts)
                .Include(o => o.MentorProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByMentorWithDetailsAsync(int mentorProfileId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.MentorProfileId == mentorProfileId)
                .Include(o => o.CompanyProfile)
                    .ThenInclude(cp => cp.Contacts)
                .Include(o => o.MentorProfile)
                .Include(o => o.ProjectStudents)
                    .ThenInclude(ps => ps.Student)
                .Include(o => o.ProjectTasks)
                .Include(o => o.Feedbacks)
                .ToListAsync();
        }
    }
}