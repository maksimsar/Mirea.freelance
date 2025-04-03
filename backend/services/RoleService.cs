using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;

namespace Mirea.freelance.backend.services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly AppDbContext _context; // Добавляем контекст для работы с UserRoles

    public RoleService(IRoleRepository roleRepository, AppDbContext context)
    {
        _roleRepository = roleRepository;
        _context = context;
    }

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        return await _roleRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllAsync();
    }

    public async Task<(bool success, string message, Role? role)> CreateRoleAsync(string name)
    {
        bool nameTaken = await _roleRepository.IsRoleNameTakenAsync(name);
        if (nameTaken)
        {
            return (false, "Role name is already taken.", null);
        }

        var newRole = new Role
        {
            Name = name
        };

        await _roleRepository.AddAsync(newRole);
        return (true, "Role created successfully.", newRole);
    }

    public async Task<(bool success, string message, Role? role)> UpdateRoleAsync(int id, string newName)
    {
        var existingRole = await _roleRepository.GetByIdAsync(id);
        if (existingRole == null)
            return (false, "Role not found.", null);

        bool nameTaken = await _roleRepository.IsRoleNameTakenAsync(newName);
        if (nameTaken && existingRole.Name != newName)
        {
            return (false, "Role name is already taken.", null);
        }

        existingRole.Name = newName;
        await _roleRepository.UpdateAsync(existingRole);
        return (true, "Role updated successfully.", existingRole);
    }

    public async Task<(bool success, string message)> DeleteRoleAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
        {
            return (false, "Role not found.");
        }

        await _roleRepository.DeleteAsync(id);
        return (true, "Role deleted successfully.");
    }

    public async Task<(bool success, string message, UserRole? userRole)> AssignRoleToUserAsync(int userId, int roleId)
    {
        var user = await _context.Users.FindAsync(userId);
        var role = await _roleRepository.GetByIdAsync(roleId);

        if (user == null) return (false, "User not found.", null);
        if (role == null) return (false, "Role not found.", null);

        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId,
            AssignedDate = DateTime.UtcNow
        };

        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();

        return (true, "Role assigned successfully.", userRole);
    }
}