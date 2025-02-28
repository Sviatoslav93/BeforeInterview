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
            .IsRequired(false)
            .HasMaxLength(Deal.NotesMaxLength);

        builder.HasOne<Store>()
            .WithMany()
            .HasForeignKey(x => x.StoreId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(x => x.Products, p =>
        {
            p.ToTable("DealProducts");
            p.WithOwner().HasForeignKey("DealId");
            p.Property(x => x.ProductId).IsRequired();
            p.Property(x => x.Quantity).IsRequired();
        });
    }
}
