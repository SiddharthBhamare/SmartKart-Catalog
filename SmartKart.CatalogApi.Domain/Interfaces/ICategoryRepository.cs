using SmartKart.CatalogApi.Domain.Entities;

namespace SmartKart.CatalogApi.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetByNameAsync(string name);
    }
}
