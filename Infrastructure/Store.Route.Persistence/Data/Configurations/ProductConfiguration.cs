using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Route.Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasColumnType(typeName: "varchar").HasMaxLength(256);
            builder.Property(P => P.Description).HasColumnType("varchar").HasMaxLength(512);
            builder.Property(P => P.PictureUrl).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(P => P.Type)
                   .WithMany()
                   .HasForeignKey(P => P.TypeId)
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
