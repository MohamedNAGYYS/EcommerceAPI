/*
I have some jobs need to build, just defining their names here:
add to cart, update, delete from ,clear cart, get cart and its items
*/

using EcommerceAPI.DTOs;

namespace EcommerceAPI.Service
{
    public interface ICartService
    {
        Task<CartResponseDto> GetItemsService(int UserID);
        Task<bool> AddToCartService(int UserID, AddToCartDto dto);
        Task<bool> UpdateFromCartService(int UserID, UpdateFromCartDto dto);
        Task<bool> DeleteFromCartService(int UserID, int ProductID);
        Task<bool> ClearCartService(int UserID);
        
    }
}