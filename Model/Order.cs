// In orders, there are some data like userid, the date, total status, orderitems


using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Model
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
    
    
        // User
        public int UserID { get; set; }
        public User User { get; set; } = null!;

        // Date
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;


        // Total price
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}