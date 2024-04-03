using Core.Services;
using Microsoft.AspNetCore.Mvc;
using S3DB_Individual_Project_Tony.CustomFilter;

namespace S3DB_Individual_Project_Tony.Controllers;

[ServiceFilter(typeof(CustomExceptionFilter))]
[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleService _service;

    public RoleController(RoleService roleService)
    {
        _service = roleService;
    }

    [HttpPost("")]
    public async Task<ActionResult> Create(string roleName)
    {
        var role = await _service.CreateRole(roleName);
        return Ok(role);
    }

    [HttpPost("Assign")]
    public async Task<ActionResult> AssignRole(string roleName, string userId)
    {
        var role = await _service.AssignRoleToUser(roleName, userId);
        return Ok(role);
    }


    [HttpPost("Remove")]
    public async Task<ActionResult> RemoveRole(string roleName, string userId)
    {
        var role = await _service.RemoveRoleFromUser(roleName, userId);
        return Ok(role);
    }
}