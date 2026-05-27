// all what I need is name

using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; } = string.Empty;
    }
}