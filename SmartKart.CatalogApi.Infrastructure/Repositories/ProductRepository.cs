using Microsoft.EntityFrameworkCore;
using SmartKart.CatalogApi.Domain.Entities;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Infrastructure.DBContext;

namespace SmartKart.CatalogApi.Infrastructure.Repositories
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            return await _dbContext.Products
                                   .Where(p => p.CategoryId == categoryId)
                                   .ToListAsync();
        }
        public IQueryable<Product> AsQueryable()
        {
            return _dbContext.Products.AsQueryable();
        }
    }
}
