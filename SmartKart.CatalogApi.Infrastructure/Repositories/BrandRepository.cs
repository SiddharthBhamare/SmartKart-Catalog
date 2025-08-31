using Microsoft.EntityFrameworkCore;
using SmartKart.CatalogApi.Domain.Entities;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Infrastructure.DBContext;

namespace SmartKart.CatalogApi.Infrastructure.Repositories
{
    public class BrandRepository : EfRepository<Brand>, IBrandRepository
    {
        public BrandRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IReadOnlyList<Brand>> GetByNameAsync(string name)
        {
            return await _dbContext.Brands.Where(x => x.Name == name).ToListAsync();
        }
    }
}
