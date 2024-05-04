using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class RoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository roleRepository)
    {
        _repository = roleRepository;
    }

    public Task<ApplicationUser?> AssignRoleToUser(string roleName, string userId)
    {
        return _repository.AssignRoleToUser(roleName, userId);
    }

    public Task<IdentityRole> CreateRole(string roleName)
    {
        var existingRole = _repository.GetRoleBy(roleName);
        if (existingRole != null) throw new InvalidOperationException("Can't create a role that already exists!");
        return _repository.CreateRole(roleName);
    }

    public Task<ApplicationUser?> RemoveRoleFromUser(string roleName, string userId)
    {
        return _repository.RemoveRoleFromUser(roleName, userId);
    }
}