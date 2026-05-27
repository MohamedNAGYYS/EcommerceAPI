// I need to response, product id, product name, quantity, price, total



namespace EcommerceAPI.DTOs
{
    public class CartItemResponseDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; }
        public decimal Total  => Quantity * UnitPrice; 
    }
}