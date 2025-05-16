using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mirea.freelance.backend.data;
using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleRepository(AppDbContext context, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
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
        var user = await _userManager.FindByIdAsync(userRole.UserId.ToString());
        var role = await _roleManager.FindByIdAsync(userRole.RoleId.ToString());
        // Добавляем роль в IdentityUserRoles через UserManager
        var result = await _userManager.AddToRoleAsync(user, role.Name);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to assign role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Добавляем запись в кастомную таблицу UserRoles
        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserRoleAsync(UserRole userRole)
    {
        // Проверяем, существует ли запись
        var existingUserRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.Id == userRole.Id);
        if (existingUserRole == null)
        {
            throw new Exception("User role assignment not found");
        }

        // Если RoleId изменился, обновляем IdentityUserRoles
        if (existingUserRole.RoleId != userRole.RoleId)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId.ToString());
            var oldRole = await _roleManager.FindByIdAsync(existingUserRole.RoleId.ToString());
            var newRole = await _roleManager.FindByIdAsync(userRole.RoleId.ToString());
            if (user == null || oldRole == null || newRole == null)
            {
                throw new Exception("User or role not found");
            }

            // Удаляем старую роль
            var removeResult = await _userManager.RemoveFromRoleAsync(user, oldRole.Name);
            if (!removeResult.Succeeded)
            {
                throw new Exception($"Failed to remove old role: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
            }

            // Добавляем новую роль
            var addResult = await _userManager.AddToRoleAsync(user, newRole.Name);
            if (!addResult.Succeeded)
            {
                throw new Exception($"Failed to assign new role: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");
            }
        }
    }

    // Удалить назначение роли
    public async Task DeleteUserRoleAsync(int id)
    {
        var userRole = await GetUserRoleByIdAsync(id);
        if (userRole != null)
        {
            // Удаляем роль из IdentityUserRoles
            var user = await _userManager.FindByIdAsync(userRole.UserId.ToString());
            var role = await _roleManager.FindByIdAsync(userRole.RoleId.ToString());
            if (user != null && role != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to remove role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            // Удаляем запись из кастомной таблицы UserRoles
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