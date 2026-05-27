// Cart has Many items, and One user
// Cart + User = One-To-One
// Cart + Item = One-To-Many
// Cart == ID, UserID

using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Model
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        // [Range(1, int.MaxValue, ErrorMessage = "IDs must be greater than 0")]
        // [Required(ErrorMessage = "UserID is required")]
        public int UserID { get; set; }
        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}