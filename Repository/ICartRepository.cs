/*
Add some items to DB
Update from DB, get the items and update them which would be product id, and quantity
Return Items as a response that includes id, name, price, quantity, total
delete items from cart
delete entire cart
*/


using EcommerceAPI.DTOs;
using EcommerceAPI.Model;

namespace EcommerceAPI.Repository
{
    public interface ICartRepository
    {
        Task<Cart?> FindCartRepo(int UserID);
        Task<Product?> FindItemRepo(int ProductID);


        Task<bool> AddToCartRepo(int UserID, AddToCartDto dto);
        Task<bool> UpdateFromCartRepo(int UserID, UpdateFromCartDto dto);
        Task<bool> DeleteFromCartRepo(int UserID, int ProductID);
        Task<bool> ClearCartRepo(int UserID);
        Task<CartResponseDto> GetItemsRepo(int CartID);
    }
}