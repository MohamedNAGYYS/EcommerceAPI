// User must enter: Email, Password

using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class LoginDto
    {
        [EmailAddress(ErrorMessage = "Email is invalid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

    }
}