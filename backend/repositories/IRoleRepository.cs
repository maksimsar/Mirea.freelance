using Mirea.freelance.backend.models;

namespace Mirea.freelance.backend.repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(int id);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(int id);
    Task<bool> IsRoleNameTakenAsync(string name);
}