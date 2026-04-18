using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Identity;
using Store.Route.Services.Abstractions;
using Store.Route.Services.Abstractions.Auth;
using Store.Route.Services.Abstractions.Baskets;
using Store.Route.Services.Abstractions.Cache;
using Store.Route.Services.Abstractions.Order;
using Store.Route.Services.Abstractions.Payments;
using Store.Route.Services.Abstractions.Products;
using Store.Route.Services.Auth;
using Store.Route.Services.Baskets;
using Store.Route.Services.Cache;
using Store.Route.Services.Orders;
using Store.Route.Services.Payments;
using Store.Route.Services.Products;
using Store.Route.Shared;

namespace Store.Route.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IBasketRepository _basketRepository, IMapper _mapper, ICacheRepository _cacheRepository, UserManager<AppUser> _userManager, IOptions<JWTOptions> Options,
        IConfiguration configuration) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);
        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);
        public IAuthService AuthService { get; } = new AuthService(_userManager, Options, _mapper);

        public IOrderSevice OrderService { get; } = new OrderService(_unitOfWork, _basketRepository, _mapper);

        public IPaymentService PaymentService { get; } = new PaymentService(_basketRepository, _unitOfWork, configuration, _mapper);
    }
}
