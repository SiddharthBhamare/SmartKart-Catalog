using SmartKart.CatalogApi.Application.Dto;
using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task AddProductAsync(ProductDto product);
        Task AddProductsAsync(List<ProductDto> products);
        Task UpdateProductAsync(ProductDto product);
        Task DeleteProductAsync(Guid id);
        Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId);
    }
}
