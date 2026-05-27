// Admins need to:
// GetAllProducts, GetByID, Create or Add Products, Update, or DeleteProducts


using EcommerceAPI.DTOs;

namespace EcommerceAPI.Service
{
    public interface IProductService
    {
        Task<List<ResponseProductDto>> GetProductsService();
        Task<ResponseProductDto?> GetProductByIDService(int ProductID);
        Task<bool> CreateProductService(CreateProductDto dto);
        Task<bool> UpdateProductService(int ProductID, UpdateProductDto dto);
        Task<bool> DeleteProductService(int ProductID);
        Task<PagedResultDto<ResponseProductDto>> GetProductsPagedService(PaginationParamsDto pagination);
    }
}