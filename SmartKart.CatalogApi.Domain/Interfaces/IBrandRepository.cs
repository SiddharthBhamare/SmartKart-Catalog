using SmartKart.CatalogApi.Domain.Entities;
using System.Collections.Generic;

namespace SmartKart.CatalogApi.Domain.Interfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<IReadOnlyList<Brand>> GetByNameAsync(string name);
    }
}
