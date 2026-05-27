using EcommerceAPI.DTOs;
using EcommerceAPI.Repository;

namespace EcommerceAPI.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        public CartService(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }



        // Get items from cart
        public async Task<CartResponseDto> GetItemsService(int UserID)
        {
            var cart = await _cartRepo.FindCartRepo(UserID);
            if (cart == null){ throw new KeyNotFoundException("Cart not found"); }
            else
            {
                return await _cartRepo.GetItemsRepo(cart.CartID);
            }
        }



        // Add to cart
        public async Task<bool> AddToCartService(int UserID, AddToCartDto dto)
        {
            var cart = await _cartRepo.FindCartRepo(UserID);
            if (cart == null){ throw new KeyNotFoundException("Cart not found"); }


            var product = await _cartRepo.FindItemRepo(dto.ProductID);
            if (product == null) {throw new KeyNotFoundException("Product not found"); } 

            if (product.Stock < dto.Quantity)
            {
                throw new Exception("Invalid input! Quantity is greater than stock."); 
            }

            return await _cartRepo.AddToCartRepo(UserID, dto);
            
        }

        // Update from Cart
        public async Task<bool> UpdateFromCartService(int UserID, UpdateFromCartDto dto)
        {
            // find cart
            var cart = await _cartRepo.FindCartRepo(UserID);
            if (cart == null){ throw new KeyNotFoundException("Cart not found"); }
            else
            {
                // find product
                var product = await _cartRepo.FindItemRepo(dto.ProductID);
                if (product == null){ throw new KeyNotFoundException("Product not found"); }
                else
                {
                    // Update
                    await _cartRepo.UpdateFromCartRepo(UserID, dto);
                    // return true
                    return true;
                    
                }
            
                
            }
        }
    
        // Delete From cart
        public async Task<bool> DeleteFromCartService(int UserID, int ProductID)
        {
            // Find cart -> Find product -> delete it -> return true

            var cart = await _cartRepo.FindCartRepo(UserID);
            if (cart == null){ throw new KeyNotFoundException("Cart not found"); }
            else
            {
                var product = await _cartRepo.FindItemRepo(ProductID);
                if (product == null){ throw new KeyNotFoundException("Product not found"); }
                else
                {
                    await _cartRepo.DeleteFromCartRepo(UserID, ProductID);
                    return true;
                }
            }
        }

        // Clear cart
        public async Task<bool> ClearCartService(int UserID)
        {
            var result = await _cartRepo.ClearCartRepo(UserID);
            if (!result) throw new KeyNotFoundException("Cart not found");
            return true;
                        
        }
    
    }
}