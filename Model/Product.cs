// Product table: ID, Name, Description, Price, Stock, CreatedAT, CategoryID


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Model
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; } 

       
        [ForeignKey(nameof(Category))]
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;
        
        
        // Name, required
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; } = string.Empty;
        

        // Description, optional
        public string Description { get; set; } = string.Empty;

        // Price, decimal, required, must not be zero
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        // Stock, required, it is okay to be zero.. products might be done
        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }

        // Date
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }  
}