using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(256);
        builder.Property(e => e.Phone).IsRequired().HasMaxLength(20);
        builder.Property(e => e.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Role).IsRequired().HasConversion<string>().HasMaxLength(50);
        builder.Property(e => e.ProfileImageUrl).HasMaxLength(500);
        builder.Property(e => e.RefreshToken).HasMaxLength(500);

        builder.Ignore(e => e.FullName);

        builder.HasIndex(e => e.Email).IsUnique();
        builder.HasIndex(e => e.Role);

        // 1-to-1 relationships configured on the other side
    }
}
