// Implement what I defined in ICart...


using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class CartRepository:ICartRepository
    {
        // Inject DB
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Cart?> FindCartRepo(int UserID) =>
            await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserID == UserID);
        public async Task<Product?> FindItemRepo(int ProductID) =>
            await _context.Products.FindAsync(ProductID);


        /*
        Add to cart:
            take user's input which is product id, and quantity
            get price from product
            get cart by userid from user
            create a cartitem object
            add it, and save, return true
        */ 

        public async Task<bool> AddToCartRepo(int UserID, AddToCartDto dto)
        {
            // if (cart == null){ return false; }

            
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserID == UserID);
            var product = await _context.Products.FindAsync(dto.ProductID);
            
            var existingItem = cart.CartItems.FirstOrDefault(e => e.ProductID == dto.ProductID);

            if (existingItem != null)
            { 
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                
                var newItems = new CartItem
                {
                    CartID = cart.CartID,
                    ProductID = dto.ProductID,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price
                };

                _context.CartItems.Add(newItems);
            
            }
            await _context.SaveChangesAsync();
            return true;
        }


        // Update from cart
            // Get the cart you want to update from by user id
            // check if it exists
            // check if product exists
            // update, save, return true
        public async Task<bool> UpdateFromCartRepo(int UserID, UpdateFromCartDto dto)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserID==UserID);
            if (cart == null){ return false; }

            var existingProduct = cart.CartItems.FirstOrDefault(c => c.ProductID == dto.ProductID);
            if (existingProduct == null){ return false; }
            else
            {
                // existingProduct.ProductID = dto.ProductID; 
                existingProduct.Quantity = dto.Quantity;
            }
            await _context.SaveChangesAsync();
            return true;
        }


        // Get items
        public async Task<CartResponseDto> GetItemsRepo(int CartID)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.CartID == CartID);
            if (cart == null){ return null; }

            var items = cart.CartItems.Select(ci => new CartItemResponseDto
            {
                ProductID = ci.ProductID,
                ProductName = ci.Product.ProductName,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice
            }).ToList();

            return new CartResponseDto
            {
                CartID = cart.CartID,
                Items = items    
            };
        }


        // Delete from cart
        public async Task<bool> DeleteFromCartRepo(int UserID, int ProductID)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserID == UserID);
            if (cart == null){ return false; }

            var items = cart.CartItems.FirstOrDefault(ci => ci.ProductID == ProductID);
            if (items == null){ return false; }

            _context.CartItems.Remove(items);
            await _context.SaveChangesAsync();
            return true;

        }


        // Delete cart itself
        public async Task<bool> ClearCartRepo(int UserID)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserID == UserID);
            if (cart == null){ return false; }

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return true;
        }
    
    }

}