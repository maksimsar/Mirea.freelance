using Mirea.freelance.backend.models;
using Mirea.freelance.backend.repositories;
using Mirea.freelance.backend.dto;

namespace Mirea.freelance.backend.services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    // Получить роль по Id
    public async Task<RoleResponseDto?> GetRoleByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;

        return new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name
        };
    }

    // Получить все роли
    public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => new RoleResponseDto
        {
            Id = r.Id,
            Name = r.Name
        });
    }

    // Создать роль
    public async Task<(bool success, string message, RoleResponseDto? role)> CreateRoleAsync(CreateRoleDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return (false, "Название роли не может быть пустым.", null);

        var role = new Role
        {
            Name = dto.Name
        };

        await _roleRepository.AddAsync(role);

        var response = new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name
        };

        return (true, "Роль успешно создана.", response);
    }

    // Обновить роль
    public async Task<(bool success, string message, RoleResponseDto? role)> UpdateRoleAsync(int id, UpdateRoleDto dto)
    {
        var existingRole = await _roleRepository.GetByIdAsync(id);
        if (existingRole == null)
            return (false, "Роль не найдена.", null);

        if (string.IsNullOrWhiteSpace(dto.NewName))
            return (false, "Новое название роли не может быть пустым.", null);

        existingRole.Name = dto.NewName;

        await _roleRepository.UpdateAsync(existingRole);

        var response = new RoleResponseDto
        {
            Id = existingRole.Id,
            Name = existingRole.Name
        };

        return (true, "Роль успешно обновлена.", response);
    }

    // Удалить роль
    public async Task<(bool success, string message)> DeleteRoleAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return (false, "Роль не найдена.");

        await _roleRepository.DeleteAsync(id);
        return (true, "Роль успешно удалена.");
    }

    // Получить назначение роли по Id
    public async Task<UserRoleResponseDto?> GetUserRoleByIdAsync(int id)
    {
        var userRole = await _roleRepository.GetUserRoleByIdAsync(id);
        if (userRole == null) return null;

        return new UserRoleResponseDto
        {
            Id = userRole.Id,
            UserId = userRole.UserId,
            RoleId = userRole.RoleId,
            AssignedDate = userRole.AssignedDate
        };
    }

    // Получить все роли пользователя
    public async Task<IEnumerable<UserRoleResponseDto>> GetRolesByUserIdAsync(int userId)
    {
        var userRoles = await _roleRepository.GetRolesByUserIdAsync(userId);
        return userRoles.Select(ur => new UserRoleResponseDto
        {
            Id = ur.Id,
            UserId = ur.UserId,
            RoleId = ur.RoleId,
            AssignedDate = ur.AssignedDate
        });
    }

    // Назначить роль пользователю
    public async Task<(bool success, string message, UserRoleResponseDto? userRole)> CreateUserRoleAsync(CreateUserRoleDto dto)
    {
        var existingRole = await _roleRepository.GetByIdAsync(dto.RoleId);
        if (existingRole == null)
            return (false, "Указанная роль не найдена.", null);

        var userRole = new UserRole
        {
            UserId = dto.UserId,
            RoleId = dto.RoleId,
            AssignedDate = DateTime.UtcNow
        };

        await _roleRepository.AddUserRoleAsync(userRole);

        var response = new UserRoleResponseDto
        {
            Id = userRole.Id,
            UserId = userRole.UserId,
            RoleId = userRole.RoleId,
            AssignedDate = userRole.AssignedDate
        };

        return (true, "Роль успешно назначена пользователю.", response);
    }

    // Обновить назначение роли
    public async Task<(bool success, string message, UserRoleResponseDto? userRole)> UpdateUserRoleAsync(int id, UpdateUserRoleDto dto)
    {
        var existingUserRole = await _roleRepository.GetUserRoleByIdAsync(id);
        if (existingUserRole == null)
            return (false, "Назначение роли не найдено.", null);

        var newRole = await _roleRepository.GetByIdAsync(dto.NewRoleId);
        if (newRole == null)
            return (false, "Указанная новая роль не найдена.", null);

        existingUserRole.RoleId = dto.NewRoleId;

        await _roleRepository.UpdateUserRoleAsync(existingUserRole);

        var response = new UserRoleResponseDto
        {
            Id = existingUserRole.Id,
            UserId = existingUserRole.UserId,
            RoleId = existingUserRole.RoleId,
            AssignedDate = existingUserRole.AssignedDate
        };

        return (true, "Назначение роли успешно обновлено.", response);
    }

    // Удалить назначение роли
    public async Task<(bool success, string message)> DeleteUserRoleAsync(int id)
    {
        var userRole = await _roleRepository.GetUserRoleByIdAsync(id);
        if (userRole == null)
            return (false, "Назначение роли не найдено.");

        await _roleRepository.DeleteUserRoleAsync(id);
        return (true, "Назначение роли успешно удалено.");
    }
}