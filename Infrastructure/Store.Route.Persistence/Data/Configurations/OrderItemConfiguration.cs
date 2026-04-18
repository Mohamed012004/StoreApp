using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Route.Domains.Entities.Order;

namespace Store.Route.Persistence.Data.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(Io => Io.Product);
            builder.Property(IO => IO.Price).HasColumnType("decimal(18,2)");
        }
    }
}
