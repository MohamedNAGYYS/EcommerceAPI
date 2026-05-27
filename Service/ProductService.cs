// Implement these method:
// GetAllProducts, GetByID, Create or Add Products, Update, or DeleteProducts


using EcommerceAPI.DTOs;
using EcommerceAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        // Get all
        public async Task<List<ResponseProductDto>> GetProductsService()
        {
            var products = await _repo.GetProductsRepo();
            if (!products.Any()){ throw new KeyNotFoundException("List of products is empty"); }

            return products;
        }

        // Get by id
        public async Task<ResponseProductDto?> GetProductByIDService(int ProductID)
        {
            var product = await _repo.GetProductByIDRepo(ProductID);
            if (product == null){ throw new KeyNotFoundException("List of products is empty"); }
            return product;

        }


        // Create
        public async Task<bool> CreateProductService(CreateProductDto dto)
        {
            var product = await _repo.CreateProductRepo(dto);
            if(!product){ throw new Exception("Invalid Input"); }
            return true;
        }

        // Update
        public async Task<bool> UpdateProductService(int ProductID, UpdateProductDto dto)
        {
            var product = await _repo.UpdateProductRepo(ProductID, dto);
            if (!product){ throw new KeyNotFoundException("Product not found"); }
            return true;
        }

        // DeleteProduct
        public async Task<bool> DeleteProductService(int ProductID)
        {
            var product = await _repo.DeleteProductRepo(ProductID);
            if (!product){ throw new KeyNotFoundException("Product not found"); }
            return true;

        } 


        public async Task<PagedResultDto<ResponseProductDto>> GetProductsPagedService([FromBody] PaginationParamsDto pagination)
        {
            return await _repo.GetProductsPagedRepo(pagination);
        }  
    }
}