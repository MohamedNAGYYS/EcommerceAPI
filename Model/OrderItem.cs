using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Model
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; } 
    
    
        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductID { get; set; } 
        public Product Product { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "UnitPrice is required")]
        public decimal UnitPrice{ get; set; }
    }
    
}