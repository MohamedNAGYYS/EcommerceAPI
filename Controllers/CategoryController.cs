// Endpoitns for: get all, get by id or name, create, update, delete

using EcommerceAPI.Service;
using EcommerceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase{
        
        
        private readonly ICategoryService _categoryService;
        public CategoryController (ICategoryService categoryService){
            _categoryService = categoryService;
        }


        // get all
        [HttpGet]
        public async Task<ActionResult<List<ResponseCategoryDto>>> GetCategoriesController()
        {
            try
            {
                return Ok(await _categoryService.GetCategoriesService());
            }catch(KeyNotFoundException ex) { return NotFound(ex.Message); }

        }


        [HttpGet("{CategoryID}")]
        public async Task<ActionResult<ResponseCategoryDto>> GetCategoryByIDController(int CategoryID)
        {
            try
            {
                return Ok(await _categoryService.GetCategoryByIDService(CategoryID));
            }catch(KeyNotFoundException ex) { return NotFound(ex.Message); }
        } 


        // Create 
        [HttpPost("create_category")]
        public async Task<ActionResult> CreateCategoryController([FromBody] CreateCategoryDto dto)
        {
            try
            {
                var result = await _categoryService.CreateCategoryService(dto);
                return Ok(new {message= "Category has been created successfully"});
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }


        // UpdateCategory
        [HttpPut("update_category/{CategoryID}")]
        public async Task<ActionResult> UpdateCategoryController(int CategoryID, [FromBody] UpdateCategoryDto dto)
        {
            try
            {
                var result = await _categoryService.UpdateCategoryService(CategoryID, dto);
                return Ok(result);
            }
            catch(KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        // DeleteCategory
        [HttpDelete("delete_category/{CategoryID}")]
        public async Task<ActionResult> DeleteCategoryController(int CategoryID)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryService(CategoryID);
                return NoContent();
            }
            catch(KeyNotFoundException ex) { return NotFound(ex.Message); }
        }



    }
    
}