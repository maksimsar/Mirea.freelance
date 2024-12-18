using Microsoft.EntityFrameworkCore;
using Mirea.Freelance.backend.data;

public class RoleService
{
    private readonly AppDbContext _context;
    
    public RoleService(AppDbContext context)
    {
        _context=context;
    }

    public async Task<Role> CreateRoleAsync(Role role)
    {
    // Проверяем, существует ли уже такая роль в базе данных
    var existingRole = await _context.Roles
        .FirstOrDefaultAsync(r => r.Name == role.Name);

    if (existingRole != null)
    {
        return null;
    }

    _context.Roles.Add(role);
    await _context.SaveChangesAsync();

    return role;
    }

    public async Task<Role> UpdateRoleAsync(int id, Role updatedRole)
    {
        var existingRole = await _context.Roles.FindAsync(id);
        if (existingRole == null) return null;

        existingRole.Name = updatedRole.Name;

        await _context.SaveChangesAsync();
        return existingRole;

    }

}