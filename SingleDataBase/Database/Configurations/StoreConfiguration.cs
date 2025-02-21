using Microsoft.EntityFrameworkCore;
using SingleDataBase.Entities;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Database.Configurations;

public class StoreConfiguration(IStoreCodeProvider storeCodeProvider) : IEntityTypeConfiguration<Store>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .HasMaxLength(16);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.WebsiteUri)
            .IsRequired()
            .HasMaxLength(512);

        builder.HasMany(x => x.Deals)
            .WithOne()
            .HasForeignKey(x => x.StoreId);

        builder.HasQueryFilter(x => x.Code == storeCodeProvider.GetCurrentStoreCode());
    }
}
