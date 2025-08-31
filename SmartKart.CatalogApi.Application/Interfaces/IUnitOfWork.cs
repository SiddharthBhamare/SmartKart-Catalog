using SmartKart.CatalogApi.Domain.Interfaces;

namespace SmartKart.CatalogApi.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IBrandRepository Brands { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
