// Category: ID, Name
// The bond with Product: One-To-Many => One category has many items


using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Model
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
    
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}