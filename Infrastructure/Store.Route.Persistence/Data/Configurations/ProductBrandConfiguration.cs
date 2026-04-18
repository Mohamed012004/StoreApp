using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Route.Domains.Entities.Products;

namespace Store.Route.Persistence.Data.Configurations
{
    public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(B => B.Name).HasColumnType(typeName: "varchar").HasMaxLength(256);
        }
    }
}
