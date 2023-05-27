using Application.eServicesPortal.ApplicationServices;
using Application.eServicesPortal.DTOs;
using Application.eServicesPortal.Users.Query;
using Application.eServicesPortal.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eServices_Portal.Controllers
{
    public class AppController : ControllerBase
    {
        private readonly AppService _service;
        public AppController(AppService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("Get-User")]
        public async Task<IActionResult> GetUser([FromQuery] GetUserQuery query, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetUser(query));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Get-Users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _service.GetUsers());
        }

    }
}
