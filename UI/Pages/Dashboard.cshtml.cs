using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UI.Models;

namespace UI.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly HttpClient httpClient;
        public string UserId { get; private set; }
        public string Token { get; private set; }
        public string Role { get; private set; }
        public bool IsCustomer { get; set; }
        public bool IsAdmin { get; set; }

        public DashboardModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public User User { get; private set; }
        public List<User> Users { get; private set; }
        public async Task OnGetAsync()
        {
            string getUserEndpoint;
            // Retrieve the JWT token or use any other authentication mechanism
            UserId = TempData["UserId"]?.ToString();
            Token = TempData["Token"]?.ToString();
            Role = TempData["Role"]?.ToString();

            if (Role == "Admin")
            {
                getUserEndpoint = "http://localhost:5275/Get-Users";
                IsAdmin = true; }
            else
            {
                 getUserEndpoint = "http://localhost:5275/Get-User?UserId=" + UserId;
                IsCustomer = true;
            }
            // Make a GET request to the Get-User endpoint
           
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var response = await httpClient.GetAsync(getUserEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var userJson = await response.Content.ReadAsStringAsync();
                if (IsCustomer)
                {
                    Console.WriteLine(response.Content);


                    

                    User = JsonConvert.DeserializeObject<User>(userJson);
                }

                else if(IsAdmin)
                {
                    Users = JsonConvert.DeserializeObject<List<User>>(userJson);
                }

                Console.WriteLine(response.Content);

                // Parse the user information and check the roles
                // Set the IsCustomer and IsAdmin properties accordingly
            }
        }
    }
}
