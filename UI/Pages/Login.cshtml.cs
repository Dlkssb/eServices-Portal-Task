using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient httpClient;

        public LoginModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            // Make a POST request to the login endpoint
            var loginEndpoint = "http://localhost:5275/login";
            var loginData = new { username, password };
            var response = await httpClient.PostAsJsonAsync(loginEndpoint, loginData);

            if (response.IsSuccessStatusCode)
            {
                var tokenString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Token: " + tokenString);
                
                // Parse the JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(tokenString);

                // Retrieve the claims from the token
                var claims = token.Claims;

                // Use the claims as needed in your application
                var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                


                // Store the token or perform any other necessary actions
                if (userId != null || role!=null) {
                    TempData["UserId"] = userId;
                    TempData["Token"] = tokenString;
                    TempData["Role"] = role;
                    return RedirectToPage("/Dashboard"); }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid credentials.");
                    return Page();
                }
                
            }
            else
            {
                // Handle login failure
                ModelState.AddModelError(string.Empty, "Invalid credentials.");
                return Page();
            }
        }
    }
}
