using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;

namespace Mirea.freelance.backend.repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    // Получить роль по Id
    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    // Получить все роли
    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .ToListAsync();
    }

    // Добавить роль
    public async Task AddAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
    }

    // Обновить роль
    public async Task UpdateAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }

    // Удалить роль
    public async Task DeleteAsync(int id)
    {
        var role = await GetByIdAsync(id);
        if (role != null)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }

    // Получить назначение роли по Id
    public async Task<UserRole?> GetUserRoleByIdAsync(int id)
    {
        return await _context.UserRoles
            .Include(ur => ur.Role)
            .Include(ur => ur.User)
            .FirstOrDefaultAsync(ur => ur.Id == id);
    }

    // Получить все роли пользователя
    public async Task<IEnumerable<UserRole>> GetRolesByUserIdAsync(int userId)
    {
        return await _context.UserRoles
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .ToListAsync();
    }

    // Добавить назначение роли
    public async Task AddUserRoleAsync(UserRole userRole)
    {
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
    }

    // Обновить назначение роли
    public async Task UpdateUserRoleAsync(UserRole userRole)
    {
        _context.UserRoles.Update(userRole);
        await _context.SaveChangesAsync();
    }

    // Удалить назначение роли
    public async Task DeleteUserRoleAsync(int id)
    {
        var userRole = await GetUserRoleByIdAsync(id);
        if (userRole != null)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }
    }
}