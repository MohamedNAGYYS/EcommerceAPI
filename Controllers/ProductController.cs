/*
Endpoints for:
Getall , get by id , create, update, delete
*/

using EcommerceAPI.DTOs;
using EcommerceAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ResponseProductDto>>> GetProductsController()
        {
            try
            {
                return await _productService.GetProductsService();
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }

        [HttpGet("product/{ProductID}")]
        public async Task<ActionResult<ResponseProductDto>> GetProductByIDController(int ProductID)
        {
            try
            {
                var product = await _productService.GetProductByIDService(ProductID);
                return Ok(product);
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }

        // api/products/create
        [HttpPost("add_product")]
        public async Task<ActionResult> CreateProductController([FromBody] CreateProductDto dto)
        {
            try
            {
                var result = await _productService.CreateProductService(dto);
                return Ok(new { message = "Product has been created successfully" });
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }


        // api/products/update/id
        [HttpPut("update/{ProductID}")]
        public async Task<ActionResult> UpdateProductContorller(int ProductID, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var result = await _productService.UpdateProductService(ProductID, dto);
                return Ok(result);
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }



        // api/products/delete/id
        [HttpDelete("delete/{ProductID}")]
        public async Task<ActionResult> DeleteProductContorller(int ProductID)
        {
            try
            {
                var result = await _productService.DeleteProductService(ProductID);
                return NoContent();
            }
            catch(KeyNotFoundException ex){ return NotFound(ex.Message); }
        }




        [HttpGet("paged")]
        public async Task<ActionResult<PagedResultDto<ResponseProductDto>>> GetProductsPaged([FromQuery] PaginationParamsDto pagination)
        {
            var result = await _productService.GetProductsPagedService(pagination);
            return Ok(result);
        }

    }
}