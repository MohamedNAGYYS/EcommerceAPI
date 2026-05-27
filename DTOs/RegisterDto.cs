// User must enter: Email, Role, Password, ConfirmPassword


using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class RegisterDto
    {
        [EmailAddress(ErrorMessage = "Email is invalid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        // [Required(ErrorMessage = "Balance is required")]
        // public decimal Balance { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; } = string.Empty;


    }
}