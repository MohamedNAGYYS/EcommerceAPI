// Here it gets current number of page and the size


namespace EcommerceAPI.DTOs
{
    public class PaginationParamsDto
    {
        public int PageNumber { get; set; } = 1; // Default is 1
        public int PageSize { get; set; } = 10; // Default is 10
    }
}