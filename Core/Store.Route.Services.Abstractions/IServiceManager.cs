using Store.Route.Services.Abstractions.Auth;
using Store.Route.Services.Abstractions.Baskets;
using Store.Route.Services.Abstractions.Cache;
using Store.Route.Services.Abstractions.Order;
using Store.Route.Services.Abstractions.Payments;
using Store.Route.Services.Abstractions.Products;

namespace Store.Route.Services.Abstractions
{
    public interface IServiceManager
    {
        // Product Service
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public ICacheService CacheService { get; }
        public IAuthService AuthService { get; }
        public IOrderSevice OrderService { get; }
        public IPaymentService PaymentService { get; }



    }
}
