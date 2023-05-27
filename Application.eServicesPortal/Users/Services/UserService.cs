using Domain.eServicesPortal.Entities;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Domain.eServicesPortal;
using Application.eServicesPortal.DTOs;


namespace Application.eServicesPortal.Users.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<Response> Register(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return ErrorResponse("User already exists!");

            var user = new User(model.Name, model.Email, model.Mobile)
            {
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ErrorResponse("User creation failed! Please check user details and try again.");

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, UserRoles.Customer)
            };

            var claimsResult = await _userManager.AddClaimsAsync(user, authClaims);
            var roleResult = await _userManager.AddToRoleAsync(user, UserRoles.Customer);
            if (!claimsResult.Succeeded || !roleResult.Succeeded)
                return ErrorResponse(result.Errors.FirstOrDefault()?.Description);

            return SuccessResponse("User created successfully!");
        }

        public async Task<Response> RegisterAdmin(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return ErrorResponse("User already exists!");

            var user = new User(model.Name, model.Email, model.Mobile)
            {
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return ErrorResponse("User creation failed! Please check user details and try again.");

            await CreateRoleIfNotExists(UserRoles.Admin);
            await CreateRoleIfNotExists(UserRoles.Customer);

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            if (await _roleManager.RoleExistsAsync(UserRoles.Customer))
                await _userManager.AddToRoleAsync(user, UserRoles.Customer);

            return SuccessResponse("User created successfully!");
        }

        public async Task<string> Login(LoginModel login, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(login.Username);
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = GenerateToken(authClaims);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return "Wrong User Name Or password";
        }

        private async Task CreateRoleIfNotExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }

        private Response SuccessResponse(string message)
        {
            return new Response { Status = "Success", Message = message };
        }

        private Response ErrorResponse(string message)
        {
            return new Response { Status = "Error", Message = message };
        }
    }
}
