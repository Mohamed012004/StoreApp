using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order = Store.Route.Domains.Entities.Order.Order;

namespace Store.Route.Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShiningAddress);

            builder.HasOne(O => O.DeleveryMethod)
                   .WithMany()
                   .HasForeignKey(O => O.DeliveryMethodId);

            builder.HasMany(O => O.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

        }
    }
}
