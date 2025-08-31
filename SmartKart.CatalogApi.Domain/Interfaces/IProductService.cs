using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
        IQueryable<Product> AsQueryable();
    }
}
