using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Model
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
        
        public int CartID { get; set; }
        public Cart Cart { get; set; } = null!;

        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;

        [Required(ErrorMessage = "You must type how many you need this product!")]
        [Range(1, int.MaxValue, ErrorMessage = "You must type a number greater than 0")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "UnitPrice is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please type a price greater than 0.00")]
        public decimal UnitPrice { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}