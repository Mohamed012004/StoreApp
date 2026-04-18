using Store.Route.Shared.DTOs.Baskets;

namespace Store.Route.Services.Abstractions.Payments
{
    public interface IPaymentService
    {
        Task<BasketDto> CreatePaymentIntentAsync(string basketId);
    }
}
