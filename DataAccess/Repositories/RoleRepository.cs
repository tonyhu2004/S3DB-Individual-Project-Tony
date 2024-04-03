using Core.Interfaces;
using Core.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleRepository(DbContextOptions<ApplicationDbContext> options, UserManager<ApplicationUser> userManager)
    {
        _dbContext = new ApplicationDbContext(options);
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> AssignRoleToUser(string roleName, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null) await _userManager.AddToRoleAsync(user, roleName);
        return user;
    }

    public async Task<ApplicationUser?> RemoveRoleFromUser(string roleName, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null) await _userManager.RemoveFromRoleAsync(user, roleName);
        return user;
    }

    public async Task<IdentityRole> CreateRole(string roleName)
    {
        var identityRole = new IdentityRole
        {
            Name = roleName,
            NormalizedName = roleName.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        _dbContext.Roles.Add(identityRole);

        await _dbContext.SaveChangesAsync();

        return identityRole;
    }

    public IdentityRole? GetRoleBy(string roleName)
    {
        var role = _dbContext.Roles.FirstOrDefault(p => p.Name == roleName);
        return role;
    }
}