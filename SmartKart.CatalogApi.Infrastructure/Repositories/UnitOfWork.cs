using SmartKart.CatalogApi.Application.Interfaces;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Infrastructure.DBContext;

namespace SmartKart.CatalogApi.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _dbContext;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IBrandRepository Brands { get; }

        public UnitOfWork(
            CatalogDbContext dbContext,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository)
        {
            _dbContext = dbContext;
            Products = productRepository;
            Categories = categoryRepository;
            Brands = brandRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken )
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => _dbContext.Dispose();
    }

}
