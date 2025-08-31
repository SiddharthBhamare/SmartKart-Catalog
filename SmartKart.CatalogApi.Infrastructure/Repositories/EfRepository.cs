using Microsoft.EntityFrameworkCore;
using SmartKart.CatalogApi.Domain.Interfaces;
using SmartKart.CatalogApi.Infrastructure.DBContext;

namespace SmartKart.CatalogApi.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        public readonly CatalogDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EfRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
