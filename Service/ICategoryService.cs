// I am gonna define some logic jobs
// Get all categories, get by id, create, update, delete

using EcommerceAPI.DTOs;

namespace EcommerceAPI.Service
{
    public interface ICategoryService
    {
        Task<List<ResponseCategoryDto>> GetCategoriesService();
        Task<ResponseCategoryDto?> GetCategoryByIDService(int CategoryID);
        Task<bool> CreateCategoryService(CreateCategoryDto dto);
        Task<bool> UpdateCategoryService(int CategoryID, UpdateCategoryDto dto);
        Task<bool> DeleteCategoryService(int CategoryID);
    }
}