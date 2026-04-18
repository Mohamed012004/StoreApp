using Store.Route.Shared.DTOs.Orders;

namespace Store.Route.Services.Abstractions.Order
{
    public interface IOrderSevice
    {
        Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string UserEmail);

        Task<IEnumerable<DeliveryMethodsResponse>> GetAllDeliveryMethodsAsync();

        Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string UserEmail);
        Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string UserEmail);

    }
}
