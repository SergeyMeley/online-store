using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetFilteredProductsAsync(ProductFilterDto filter);
        Task<ProductDto> CreateProductAsync(ProductCreateUpdateDto dto);
        Task UpdateProductAsync(int id, ProductCreateUpdateDto dto);
        Task DeleteProductAsync(int id);
    }
}
