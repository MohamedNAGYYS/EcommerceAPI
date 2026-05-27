// Let's define some jobs to deal with DB:
// Jobs:
// Get all categories, get category by id, create category, update, delete
// Admins are capable of all of these


using EcommerceAPI.DTOs;

namespace EcommerceAPI.Repository
{
    public interface ICategoryRepository
    {
        // All Categories
        Task<List<ResponseCategoryDto>> GetCategoriesRepository();
        

        // By id
        Task<ResponseCategoryDto?> GetCategoryByIDRepository(int? CategoryID, string? CategoryName);

        // Create
        Task<bool> CreateCategoryRepository(CreateCategoryDto dto);

        // update
        Task<bool> UpdateCategoryRepository(int CategoryID, UpdateCategoryDto dto);

        // delete
        Task<bool> DeleteCategoryRepository(int CategoryID);

    }
}