using StackExchange.Redis;
using Store.Route.Domains.Contracts;
using System.Text.Json;

namespace Store.Route.Persistence.Repositories
{
    internal class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var redisValue = await _database.StringGetAsync(key);
            return redisValue;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var redisValue = await _database.StringSetAsync(key, JsonSerializer.Serialize(value), duration);
        }
    }
}
