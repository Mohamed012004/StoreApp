using Store.Route.Domains.Entities.Baskets;

namespace Store.Route.Domains.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration);
        Task<bool> DeleteBaskerAsync(string id);

    }
}
