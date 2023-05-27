using System.ComponentModel.DataAnnotations;


namespace Application.eServicesPortal.DTOs
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "User UserName is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string? Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string? Mobile { get; set; }


    }

}
