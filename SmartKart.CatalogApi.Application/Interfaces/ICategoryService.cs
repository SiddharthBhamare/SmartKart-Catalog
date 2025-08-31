using SmartKart.CatalogApi.Application.Dto;

namespace SmartKart.CatalogApi.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken ct = default);
        Task CreateCategory(CategoryDto category, CancellationToken cancellationToken);
        Task CreateCategories(List<CategoryDto> categories, CancellationToken cancellationToken);
    }
}
