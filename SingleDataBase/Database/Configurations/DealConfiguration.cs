using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingleDataBase.Entities;

namespace SingleDataBase.Database.Configurations;

public class DealConfiguration : IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> builder)
    {
        builder.ToTable("Deals");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Notes)
            .HasMaxLength(256);

        builder.HasMany(x => x.Products)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "DealProduct",
                x => x.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                x => x.HasOne<Deal>().WithMany().HasForeignKey("DealId")
            );
    }
}
