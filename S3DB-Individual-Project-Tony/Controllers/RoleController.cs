using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.ViewModels;
using System.Data.SqlClient;
using System.IO;

namespace S3DB_Individual_Project_Tony.Controllers
{
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
        public async Task<ActionResult> Post(string name)
        {
            var role = await _service.CreateRole(name);
            return Ok(role);
        }
    }
}
