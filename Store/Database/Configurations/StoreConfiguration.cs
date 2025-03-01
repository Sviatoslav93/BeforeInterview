using Microsoft.EntityFrameworkCore;
using Store.Entities;

namespace Store.Database.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Entities.Store>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Entities.Store.NameMaxLength);

        builder.Property(x => x.WebsiteUri)
            .IsRequired(false)
            .HasMaxLength(Entities.Store.WebsiteUriMaxLength);

        builder
            .HasOne<User>()
            .WithMany(x => x.Stores)
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
