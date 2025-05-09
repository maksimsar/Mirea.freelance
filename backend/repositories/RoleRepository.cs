using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleRepository(AppDbContext context, RoleManager<IdentityRole<int>> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }

    // Получить роль по Id
    public async Task<IdentityRole<int>?> GetByIdAsync(int id)
    {
        return await _roleManager.FindByIdAsync(id.ToString());
    }

    // Получить все роли
    public async Task<IEnumerable<IdentityRole<int>>> GetAllAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    // Добавить роль
    public async Task AddAsync(IdentityRole<int> role)
    {
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }


    // Обновить роль
    public async Task UpdateAsync(IdentityRole<int> role)
    {
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to update role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }

    // Удалить роль
    public async Task DeleteAsync(int id)
    {
        var role = await GetByIdAsync(id);
        if (role != null)
        {
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to delete role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
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
            .Include(ur=> ur.User)
            .Where(ur => ur.UserId == userId)
            .ToListAsync();
    }

    // Добавить назначение роли
    public async Task AddUserRoleAsync(UserRole userRole)
    {
        await _context.UserRoles.AddAsync(userRole);
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

    /* Task<IdentityRole<int>?> IRoleRepository.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<IdentityRole<int>>> IRoleRepository.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(IdentityRole<int> role)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(IdentityRole<int> role)
    {
        throw new NotImplementedException();
    }  */

}