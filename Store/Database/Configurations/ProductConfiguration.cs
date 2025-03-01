using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Entities;

namespace Store.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.StoreId, x.Name })
            .IsUnique();

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Product.NameMaxLength);

        builder.Property(x => x.Description)
            .HasMaxLength(Product.DescriptionMaxLength);

        builder.ComplexProperty(x => x.Quantity
            , q =>
            {
                q.Property(x => x.Value)
                    .HasColumnType("decimal(18, 2)");
            });

        builder.HasOne<Entities.Store>()
            .WithMany()
            .HasForeignKey(x => x.StoreId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
