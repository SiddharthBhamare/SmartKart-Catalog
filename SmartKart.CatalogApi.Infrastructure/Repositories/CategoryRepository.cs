using Microsoft.EntityFrameworkCore;
using SmartKart.CatalogApi.Domain.Entities;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Infrastructure.DBContext;

namespace SmartKart.CatalogApi.Infrastructure.Repositories
{
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IReadOnlyList<Category>> GetByNameAsync(string name)
        {
            return await _dbContext.Categories.Where(x => x.Name == name).ToListAsync();
        }
    }
}
