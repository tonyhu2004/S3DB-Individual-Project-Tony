using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace Core.UnitTest;

public class RoleServiceUnitTest
{
    [Fact]
    public async Task CreateRole_NewRole_ReturnsRole()
    {
        var roleName = "Admin";
        var identityRole = new IdentityRole
        {
            Name = roleName,
            NormalizedName = roleName.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        var mockRepository = new Mock<IRoleRepository>();
        mockRepository.Setup(repo => repo.GetRoleBy(roleName)).Returns(null as IdentityRole);
        mockRepository.Setup(repo => repo.CreateRole(roleName)).Returns(Task.FromResult(identityRole));
    
        var service = new RoleService(mockRepository.Object); 
    
        var createdRole = await service.CreateRole(roleName);
    
        Assert.NotNull(createdRole);
        Assert.Equal(roleName, createdRole.Name);
    }
    
    [Fact]
    public void CreateRole_ExistingRole_ThrowsException()
    {
        var roleName = "Admin";
        var mockRepository = new Mock<IRoleRepository>(); // Replace IRepository with the actual repository interface
        mockRepository.Setup(repo => repo.GetRoleBy(roleName)).Returns(new IdentityRole());

        var service = new RoleService(mockRepository.Object); // Inject the mocked repository into the service

        Assert.ThrowsAsync<InvalidOperationException>(async () => await service.CreateRole(roleName));
    }
    
    [Fact]
    public async Task AssignRoleToUser_ValidInput_ReturnUser()
    {
        var roleName = "Admin";
        var userId = "user1";
        var user = new ApplicationUser
        {
            Email = "user@gmail.com",
        };
        var mockRepository = new Mock<IRoleRepository>();
        mockRepository.Setup(repo => repo.AssignRoleToUser(roleName, userId)).Returns(Task.FromResult(user)!);

        var service = new RoleService(mockRepository.Object); 

        var actualUser = await service.AssignRoleToUser(roleName, userId);
        
        Assert.NotNull(actualUser);
        Assert.Equal(user.Email, actualUser!.Email);
    }
    
    [Fact]
    public async Task RemoveRoleFromUser_ValidInput_ReturnUser()
    {
        var roleName = "Admin";
        var userId = "user1";
        var user = new ApplicationUser
        {
            Email = "user@gmail.com",
        };
        var mockRepository = new Mock<IRoleRepository>(); 
        mockRepository.Setup(repo => repo.RemoveRoleFromUser(roleName, userId)).Returns(Task.FromResult(user)!);

        var service = new RoleService(mockRepository.Object);

        var actualUser = await service.RemoveRoleFromUser(roleName, userId);
        
        Assert.NotNull(actualUser);
        Assert.Equal(user.Email, actualUser!.Email);
    }
}