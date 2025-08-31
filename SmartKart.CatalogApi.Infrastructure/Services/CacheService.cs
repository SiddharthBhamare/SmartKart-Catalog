using Microsoft.Extensions.Caching.Memory;
using SmartKart.CatalogApi.Infrastructure.Interfaces;

namespace SmartKart.CatalogApi.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache) => _cache = cache;

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? absoluteExpirationRelativeToNow = null)
        {
            if (_cache.TryGetValue(key, out T value)) return value;

            value = await factory();

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow ?? TimeSpan.FromMinutes(5)
            };

            _cache.Set(key, value, options);
            return value;
        }

        public void Remove(string key) => _cache.Remove(key);
    }
}
