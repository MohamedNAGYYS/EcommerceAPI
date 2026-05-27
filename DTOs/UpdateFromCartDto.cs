// All what I need from user is the product and how many they want it, which means product id or name, and quantity


using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class UpdateFromCartDto
    {
        [Required(ErrorMessage = "You must type ProductID")]
        public int ProductID { get; set; }
        

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}