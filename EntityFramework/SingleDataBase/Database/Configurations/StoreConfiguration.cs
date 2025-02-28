using Microsoft.EntityFrameworkCore;
using SingleDataBase.Entities;

namespace SingleDataBase.Database.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Store.NameMaxLength);

        builder.Property(x => x.WebsiteUri)
            .IsRequired(false)
            .HasMaxLength(Store.WebsiteUriMaxLength);

        builder
            .HasOne<User>()
            .WithMany(x => x.Stores)
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
