using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService(IRoleRepository roleRepository) 
        {
            _repository = roleRepository;
        }

        public Task<ApplicationUser> AssignRoleToUser(string roleName, string userId)
        {
            return _repository.AssignRoleToUser(roleName, userId);
        }

        public Task<IdentityRole> CreateRole(string roleName)
        {
            return _repository.CreateRole(roleName);
        }

        public Task<ApplicationUser> RemoveRoleFromUser(string roleName, string userId)
        {
            return _repository.RemoveRoleFromUser(roleName, userId);
        }
    }
}
