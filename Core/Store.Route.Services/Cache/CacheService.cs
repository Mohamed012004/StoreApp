using Store.Route.Domains.Contracts;
using Store.Route.Services.Abstractions.Cache;

namespace Store.Route.Services.Cache
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
            var result = await _cacheRepository.GetAsync(key);
            return result;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            await _cacheRepository.SetAsync(key, value, duration);
        }
    }
}