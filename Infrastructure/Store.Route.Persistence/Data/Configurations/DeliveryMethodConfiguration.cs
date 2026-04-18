using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Route.Domains.Entities.Order;

namespace Store.Route.Persistence.Data.Configurations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D => D.ShortName).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(D => D.Description).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(D => D.DeliveryTime).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(D => D.Price).HasColumnType("decimal(18,2)");

        }
    }
}
