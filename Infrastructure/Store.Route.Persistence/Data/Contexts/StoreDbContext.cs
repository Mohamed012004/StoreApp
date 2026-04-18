using Microsoft.EntityFrameworkCore;
using Store.Route.Domains.Entities.Order;
using Store.Route.Domains.Entities.Products;
using System.Reflection;
using Order = Store.Route.Domains.Entities.Order.Order;

namespace Store.Route.Persistence.Data.Contexts
{
    public class StoreDbContext : DbContext
    {
        // CLR
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
