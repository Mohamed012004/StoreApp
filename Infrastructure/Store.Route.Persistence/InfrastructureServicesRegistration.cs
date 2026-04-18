using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.Route.Domains.Contracts;
using Store.Route.Persistence.Data.Contexts;
using Store.Route.Persistence.Identity.Contexts;
using Store.Route.Persistence.Repositories;

namespace Store.Route.Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<IdentityStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });


            services.AddScoped<IDbIntializer, DbIntializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"))
            );

            return services;
        }
    }
}
