
// Gonna implement what I defined :

using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        // DJ

        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // GetProductsRepo:
            // Get products from DB, return list
        public async Task<List<ResponseProductDto>> GetProductsRepo()
        {
            return await _context.Products.Select(c => new ResponseProductDto
            {
                CategoryID = c.CategoryID,
                CategoryName = c.Category.CategoryName,
                ProductName = c.ProductName,
                Description = c.Description,
                Price = c.Price,
                Stock = c.Stock,
                CreatedAt = c.CreatedAt
            }).ToListAsync();
        }



        // GetProductByIDRepo:
            // Get Product by id from DB -> If it is found -> Return it
        public async Task<ResponseProductDto?> GetProductByIDRepo(int ProductID)
        {
            var product = await _context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p => p.ProductID == ProductID);
            if (product == null){ return null!; }

            return new ResponseProductDto
            {
                CategoryID = product.CategoryID,
                CategoryName = product.Category.CategoryName,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt
            };
        }


        // CreateProductRepo: 
            // Task admin's request -> Create an object and set values -> Add it to DB -> Save -> return true
        public async Task<bool> CreateProductRepo(CreateProductDto dto)
        {
            var product = new Product
            {
                CategoryID = dto.CategoryID,
                ProductName = dto.ProductName,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return true;
        }

        

        // UpdateProductRepo: 
            // Get Product by id -> If it is found -> Update fields -> Save -> return true
        public async Task<bool> UpdateProductRepo(int ProductID, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(ProductID);
            if (product == null){ return false; }
            else
            {
                product.CategoryID = dto.CategoryID;
                product.ProductName = dto.ProductName;
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.Stock = dto.Stock;

                await _context.SaveChangesAsync();
                return true;
            }
        }


        // DeleteProductRepo: 
            // Get product by id -> If it is found -> remove -> save -> return true
        public async Task<bool> DeleteProductRepo(int ProductID)
        {
            var product = await _context.Products.FindAsync(ProductID);
            if (product == null){ return false; }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }







        public async Task<PagedResultDto<ResponseProductDto>> GetProductsPagedRepo(PaginationParamsDto pagination)
        {
            var query = _context.Products.Include(p => p.Category).AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query.Skip((pagination.PageNumber-1) * pagination.PageSize).Take(pagination.PageSize).Select(p => new ResponseProductDto
            {
                ProductName = p.ProductName,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category.CategoryName,
                CreatedAt = p.CreatedAt
            }).ToListAsync();


            return new PagedResultDto<ResponseProductDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }










    }
}