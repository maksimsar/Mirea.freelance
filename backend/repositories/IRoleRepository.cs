using Microsoft.AspNetCore.Identity;
using Mirea.freelance.backend.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mirea.freelance.backend.repositories;

public interface IRoleRepository
{
    Task<IdentityRole<int>?> GetByIdAsync(int id);
    Task<IEnumerable<IdentityRole<int>>> GetAllAsync();
    Task AddAsync(IdentityRole<int> role);
    Task UpdateAsync(IdentityRole<int> role);
    Task DeleteAsync(int id);
    Task<UserRole?> GetUserRoleByIdAsync(int id);
    Task<IEnumerable<UserRole>> GetRolesByUserIdAsync(int userId);
    Task AddUserRoleAsync(UserRole userRole);
    Task UpdateUserRoleAsync(UserRole userRole);
    Task DeleteUserRoleAsync(int id);
}