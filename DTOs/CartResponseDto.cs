namespace EcommerceAPI.DTOs
{
    public class CartResponseDto
    {
        public int CartID { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new();
        public decimal Total => Items.Sum(i => i.Total);
    }
}