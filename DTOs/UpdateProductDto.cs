// What I need from Admin to type:
// CategoryID, ProductName, Price, Stock, Description


using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class UpdateProductDto
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.00")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }
        
    }    
}