using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Service
{
    public class OrderService:IOrderService
    {
        private readonly AppDbContext _contextService;
        public OrderService(AppDbContext contextService)
        {
            _contextService = contextService;
        }


        public async Task<bool> CheckOut(int UserID)
        {
            var user = await _contextService.Users.FindAsync(UserID);
            if (user == null){ throw new KeyNotFoundException("User Not Found"); }
            
            var cart = await _contextService.Carts.FirstOrDefaultAsync(c => c.UserID == UserID);
            if (cart == null){ throw new KeyNotFoundException("Cart not found"); }

            var cartIitems = await _contextService.CartItems.Where(ci => ci.CartID == cart.CartID).Include(ci => ci.Product).ToListAsync();
            if (!cartIitems.Any()){ throw new Exception("No Items yet"); }


            // total
            var total = cartIitems.Sum(ci => ci.Quantity * ci.UnitPrice);

            // I gotta check if balance less than total, return exception error if yes
            if (user.Balance < total){ throw new Exception("Insufficient balance"); }

            user.Balance -= total;

            // I need to create an order
            var order = new Order{UserID=UserID,TotalAmount=total,Status="Paid",OrderDate=DateTime.UtcNow};
            _contextService.Orders.Add(order);
            await _contextService.SaveChangesAsync();


            // Gotta create orderitems
            var orderitems = cartIitems.Select(ci => new OrderItem
            {
                OrderID = order.OrderID,
                ProductID = ci.ProductID,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice
            }).ToList();
            _contextService.OrderItems.AddRange(orderitems);

            // Gotta remove all cart after paying
            _contextService.CartItems.RemoveRange(cartIitems);

            await _contextService.SaveChangesAsync();
            
            
            return true;
        }

        public async Task<List<OrderHistoryDto>> OrderHistoryService(int UserID)
        {
            var orders = await _contextService.Orders
            .Where(o => o.UserID == UserID).Include(o => o.OrderItems).ThenInclude(oi => oi.Product).OrderByDescending(o => o.OrderDate)
            .ToListAsync();

            if (!orders.Any()){ throw new KeyNotFoundException("No orders found for this user"); }

            return orders.Select(o => new OrderHistoryDto
            {
                OrderId = o.OrderID,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                OrderDate = o.OrderDate,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductID,
                    ProductName = oi.Product.ProductName,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            }).ToList();
        }       
    }
}