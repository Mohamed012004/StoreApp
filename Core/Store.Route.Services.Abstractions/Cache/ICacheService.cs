namespace Store.Route.Services.Abstractions.Cache
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object value, TimeSpan duration);

    }
}
