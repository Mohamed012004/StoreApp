using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Route.Services.Abstractions;
using Store.Route.Services.Mapping.Auth;
using Store.Route.Services.Mapping.Baskets;
using Store.Route.Services.Mapping.Orders;
using Store.Route.Services.Mapping.Products;

namespace Store.Route.Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();  // Allow Dependency Injection for ServiceManger
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));

            return services;
        }

    }
}
