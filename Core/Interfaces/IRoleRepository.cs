using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces;

public interface IRoleRepository
{
    Task<IdentityRole> CreateRole(string roleName);
    Task<IdentityUser?> AssignRoleToUser(string roleName, string userId);
    Task<IdentityUser?> RemoveRoleFromUser(string roleName, string userId);
    IdentityRole? GetRoleBy(string roleName);
}