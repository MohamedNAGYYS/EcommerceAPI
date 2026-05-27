/*
GetProductsRepo: To let user see all products and add to cart
GetProductByIDRepo: To let user know or see details about that product
CreateProductRepo: To let admin add some products
UpdateProductRepo: IF admins want to update something like stock or price
DeleteProductRepo: If admins want to delete because It is not available or something like that
*/

using EcommerceAPI.DTOs;

namespace EcommerceAPI.Repository
{
    public interface IProductRepository
    {
        Task<List<ResponseProductDto>> GetProductsRepo();
        Task<ResponseProductDto?> GetProductByIDRepo(int ProductID);
        Task<bool> CreateProductRepo(CreateProductDto dto);
        Task<bool> UpdateProductRepo(int ProductID, UpdateProductDto dto);
        Task<bool> DeleteProductRepo(int ProductID);
        Task<PagedResultDto<ResponseProductDto>> GetProductsPagedRepo(PaginationParamsDto pagination);
    }
}