namespace Store.Route.Domains.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object value, TimeSpan duration);
    }
}
