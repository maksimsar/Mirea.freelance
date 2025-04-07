using Mirea.freelance.backend.models;
namespace Mirea.freelance.backend.repositories;


public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(int id);
    Task<UserRole?> GetUserRoleByIdAsync(int id);
    Task<IEnumerable<UserRole>> GetRolesByUserIdAsync(int userId);
    Task AddUserRoleAsync(UserRole userRole);
    Task UpdateUserRoleAsync(UserRole userRole);
    Task DeleteUserRoleAsync(int id);
}