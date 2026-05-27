// all what I need is name

namespace EcommerceAPI.DTOs
{
    public class ResponseCategoryDto
    {
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; } = string.Empty;
    }
}