using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<IdentityRole> CreateRole(string roleName);
        Task<ApplicationUser> AssignRoleToUser(string roleName, string userId);
        Task<ApplicationUser> RemoveRoleFromUser(string roleName, string userId);
    }
}
