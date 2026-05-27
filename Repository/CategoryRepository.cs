// Now I am ganna implement what I defined inside ICategoryRepository. 

using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        // 1. DJ
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }


        
        // 2. Get All Categories:
        //     return the List
        public async Task<List<ResponseCategoryDto>> GetCategoriesRepository()
        {
            return await _context.Categories.Select(c => new ResponseCategoryDto
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName
            }).ToListAsync();
        }


                
        // 3. Get by id
            // get category by id
            // if it is found, return it
        public async Task<ResponseCategoryDto?> GetCategoryByIDRepository(int? CategoryID, string? CategoryName)
        {
            Category? category = null;

            if (CategoryID.HasValue)
            {
                category = await _context.Categories.FindAsync(CategoryID) ;
            }
            if (!string.IsNullOrEmpty(CategoryName))
            {
                category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == CategoryName);
            }

            if (category == null){ return null; }
            return new ResponseCategoryDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
            
        }



        // 4. Create
            // Get user's input 
            // create category (object) from Category instance
            // Add to DB
            // save
            // return true
        
        public async Task<bool> CreateCategoryRepository(CreateCategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }


        // 5. Update
            // Get category by id
            // if it is found, update fields, save, return true
            // if it is not, return false       
        public async Task<bool> UpdateCategoryRepository(int CategoryID, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(CategoryID);
            
            if (category == null){ return false; }
            
            category.CategoryName = dto.CategoryName;
            await _context.SaveChangesAsync();
            return true;
        }
    

        // 6. delete
            // get category by id
            // if it is found, remove, save, return true
            // otherwise, return false
        public async Task<bool> DeleteCategoryRepository(int CategoryID)
        {
            var category = await _context.Categories.FindAsync(CategoryID);
            
            if (category == null){ return false; }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}