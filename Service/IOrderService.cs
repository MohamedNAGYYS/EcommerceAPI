using EcommerceAPI.DTOs;

namespace EcommerceAPI.Service
{
    public interface IOrderService
    {
        Task<bool> CheckOut(int UserID);
        Task<List<OrderHistoryDto>> OrderHistoryService(int UserID);
    }
}