using Mirea.freelance.backend.models;
using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;

namespace Mirea.freelance.backend.repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        // Находит пользователя по первичному ключу (Id)
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        // Предполагается, что user уже прикреплён к контексту
        // или вы делаете Attach, FindAsync, и т.п.
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsLoginTakenAsync(string login)
    {
        return await _context.Users
            .AnyAsync(u => u.Login == login);
    }
}