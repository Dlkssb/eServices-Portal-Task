using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient httpClient;

        public RegisterModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> OnPostAsync(string username, string name, string email, string password, string mobile)
        {
            // Make a POST request to the registration endpoint
            var registrationEndpoint = "http://localhost:5275/register";
            var registrationData = new { username, name, email, password, mobile };
            var response = await httpClient.PostAsJsonAsync(registrationEndpoint, registrationData);

            if (response.IsSuccessStatusCode)
            {
                // Registration successful, redirect to login page
                return RedirectToPage("/Login");
            }
            else
            {
                // Handle registration failure
                ModelState.AddModelError(string.Empty, "Registration failed.");
                return Page();
            }
        }
    }
}
