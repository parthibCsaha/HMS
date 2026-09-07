using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
{
    public void Configure(EntityTypeBuilder<SystemSetting> builder)
    {
        builder.ToTable("system_settings");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Key).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Value).IsRequired().HasMaxLength(2000);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Category).IsRequired().HasMaxLength(50);
        builder.Property(e => e.DataType).IsRequired().HasMaxLength(20);

        builder.HasIndex(e => e.Key).IsUnique();
        builder.HasIndex(e => e.Category);
    }
}
