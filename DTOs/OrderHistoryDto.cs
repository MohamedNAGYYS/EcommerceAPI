using EcommerceAPI.DTOs;


namespace EcommerceAPI.DTOs
{
    public class OrderHistoryDto
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}