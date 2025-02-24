using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingleDataBase.Entities;

namespace SingleDataBase.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.StoreCode, x.Name })
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

        builder.HasOne<Store>()
            .WithMany()
            .HasForeignKey(x => x.StoreCode)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
