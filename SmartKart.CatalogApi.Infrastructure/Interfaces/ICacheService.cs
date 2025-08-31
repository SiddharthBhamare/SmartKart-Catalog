namespace SmartKart.CatalogApi.Infrastructure.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpirationRelativeToNow = null);
        void Remove(string key);
    }
}
