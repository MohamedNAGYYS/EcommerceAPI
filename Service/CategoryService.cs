using EcommerceAPI.Repository;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Service
{
    public class CategoryService : ICategoryService
    {
        // 1. DJ
        private readonly ICategoryRepository _categoryRepo;
        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        // Build GetCategories method
            // return ResponseCategoryDto as a ListAsync
        public async Task<List<ResponseCategoryDto>> GetCategoriesService()
        {
            var categories = await _categoryRepo.GetCategoriesRepository();
            if (!categories.Any()){ throw new KeyNotFoundException("Categories not found."); }
            return categories;
        }


        // 3. Build CategoryByID method
            // Get it by id
            // IF it is found, return it
            // If it is not, throw an exception
        public async Task<ResponseCategoryDto?> GetCategoryByIDService(int CategoryID)
        {
            var category = await _categoryRepo.GetCategoryByIDRepository(CategoryID, null);
            if (category == null){ throw new KeyNotFoundException("Category not found"); }

            return category;
        }
        


        // 4. CreateCategory:
            // Get user's input
            // Check if category name is unique or not found, or not repeated, throw an exception if it is 
            // Call CreateCategoryRepository method
            // return true
        public async Task<bool> CreateCategoryService(CreateCategoryDto dto)
        {
            var category = await _categoryRepo.GetCategoryByIDRepository(null, dto.CategoryName);
            if (category != null){ throw new Exception("That Category Name already exists"); }

            await _categoryRepo.CreateCategoryRepository(dto);
            return true;
        }


        // 5. UpdateCategory
        public async Task<bool> UpdateCategoryService(int CategoryID, UpdateCategoryDto dto)
        {
            var category = await _categoryRepo.UpdateCategoryRepository(CategoryID, dto);
            if (!category){ throw new KeyNotFoundException("Category not found"); }
            return true;
          
        }


        // 6. DeleteCategory 
        public async Task<bool> DeleteCategoryService(int CategoryID)
        {
            var category = await _categoryRepo.DeleteCategoryRepository(CategoryID);
            if (!category){ throw new KeyNotFoundException("Category not found"); }

            return true;
        }
    
    }
}