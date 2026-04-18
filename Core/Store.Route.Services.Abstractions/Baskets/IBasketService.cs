using Store.Route.Shared.DTOs.Baskets;

namespace Store.Route.Services.Abstractions.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketasync(string id);
        Task<BasketDto?> CreateBasketasync(BasketDto dto, TimeSpan duration);
        Task<bool> DeleteBasketasync(string id);
    }
}
