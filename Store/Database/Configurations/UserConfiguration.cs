using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Entities;

namespace Store.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(User.FirstNameMaxLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(User.LastNameMaxLength);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(User.EmailMaxLength);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(User.PasswordMaxLength);

        builder.Ignore(x => x.FullName);
        builder.Ignore(x => x.Age);
    }
}
