using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Model
{    
    public class User
    {
        [Key]
        public int UserID { get; set; }


        [EmailAddress(ErrorMessage = "Email is invalid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        
        
        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } = string.Empty;


        
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = "User";

        public Cart? Cart { get; set; } 


        public decimal Balance { get; set; } = 0m; // default is 0

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}