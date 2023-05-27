using Microsoft.AspNetCore.Mvc;
using Application.eServicesPortal.Users.Services;
using Application.eServicesPortal.DTOs;

namespace eServices_Portal.Controllers
{
    public class AuthenticateController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthenticateController(UserService userService)
        {
             _userService= userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
        {
           return Ok(await _userService.Login(model, cancellationToken));
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
          return Ok(await  _userService.Register(model));
        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
        return Ok( await  _userService.RegisterAdmin(model));
        }

  

    }
}
