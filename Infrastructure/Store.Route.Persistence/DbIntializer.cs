using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Route.Domains.Contracts;
using Store.Route.Domains.Entities.Identity;
using Store.Route.Domains.Entities.Order;
using Store.Route.Domains.Entities.Products;
using Store.Route.Persistence.Data.Contexts;
using Store.Route.Persistence.Identity.Contexts;
using System.Text.Json;

namespace Store.Route.Persistence
{
    public class DbIntializer(StoreDbContext _context,
           IdentityStoreDbContext _identityContext,
           UserManager<AppUser> _userManager,
           RoleManager<IdentityRole> _roleManager
           ) : IDbIntializer
    {
        //private readonly StoreDbContext _context;

        //public DbIntializer(StoreDbContext context)
        //{
        //    _context = context;
        //}
        public async Task InitializeAsync()
        {
            // Create DB
            // Update DB
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();
            }

            // Seed Data

            // ProductBrands
            if (!await _context.ProductBrands.AnyAsync())
            {

                //1. Read All Data From Json File 'brands.json'
                // E:\route\ASP .Net Core Web APIs\Project APIs\Store.Route\Infrastructure\Store.Route.Persistence\Data\DataSeeding\brands.json
                var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Route.Persistence\Data\DataSeeding\brands.json");

                //2. Convert Data From Json File To List<ProductBrands>
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                //3. Add List To DataBase
                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();

                }
            }


            // ProductTypes
            if (!await _context.ProductTypes.AnyAsync())
            {

                //1. Read All Data From Json File 'types.json'
                // E:\route\ASP .Net Core Web APIs\Project APIs\Store.Route\Infrastructure\Store.Route.Persistence\Data\DataSeeding\brands.json
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Route.Persistence\Data\DataSeeding\types.json");

                //2. Convert Data From Json File To List<ProductBrands>
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                //3. Add List To DataBase
                if (types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }
            }

            // Product
            if (!await _context.Products.AnyAsync())
            {

                //1. Read All Data From Json File 'products.json'
                // E:\route\ASP .Net Core Web APIs\Project APIs\Store.Route\Infrastructure\Store.Route.Persistence\Data\DataSeeding\brands.json
                var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Route.Persistence\Data\DataSeeding\products.json");

                //2. Convert Data From Json File To List<ProductBrands>
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                //3. Add List To DataBase
                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }
            }

            // Order
            if (!await _context.DeliveryMethods.AnyAsync())
            {

                //1. Read All Data From Json File 'products.json'
                // E:\route\ASP .Net Core Web APIs\Project APIs\Store.Route\Infrastructure\Store.Route.Persistence\Data\DataSeeding\brands.json
                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Route.Persistence\Data\DataSeeding\delivery.json");

                //2. Convert Data From Json File To List<ProductBrands>
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                //3. Add List To DataBase
                if (deliveryMethods is not null && deliveryMethods.Count > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                }
            }


            await _context.SaveChangesAsync();

        }



        public async Task InitializeIdentityAsync()
        {

            // Create DB
            // Update DB
            if (_identityContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _identityContext.Database.MigrateAsync();
            }

            // Data Seed
            if (!_identityContext.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            if (!_identityContext.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "0123445689123"
                };

                var admin = new AppUser()
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "0123445689123"
                };

                await _userManager.CreateAsync(superAdmin, "P@ssW0rd");
                await _userManager.CreateAsync(admin, "P@ssW0rd");

                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

        }
    }
}
