using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Route.Domains.Entities.Products;

namespace Store.Route.Persistence.Data.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(T => T.Name).HasColumnType(typeName: "varchar").HasMaxLength(256);
        }
    }
}
