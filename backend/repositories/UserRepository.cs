using Mirea.freelance.backend.models;
using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Microsoft.AspNetCore.Identity;

namespace Mirea.freelance.backend.repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        // Находит пользователя по первичному ключу (Id)
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User?> GetByLoginAsync(string login){
        return await _userManager.FindByNameAsync(login); //мапится на юзернейм
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public async Task AddAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded){
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task UpdateAsync(User user)
    {
        // Предполагается, что user уже прикреплён к контексту
        // или вы делаете Attach, FindAsync, и т.п.
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded){
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded){
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }

    public async Task<bool> IsLoginTakenAsync(string login)
    {
        var user = await _userManager.FindByNameAsync(login);
        return user != null;
    }

}