using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces;

public interface IRoleRepository
{
    Task<IdentityRole> CreateRole(string roleName);
    Task<ApplicationUser?> AssignRoleToUser(string roleName, string userId);
    Task<ApplicationUser?> RemoveRoleFromUser(string roleName, string userId);
    IdentityRole? GetRoleBy(string roleName);
}