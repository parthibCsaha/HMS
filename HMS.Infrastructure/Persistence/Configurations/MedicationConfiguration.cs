using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.ToTable("medications");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.GenericName).IsRequired().HasMaxLength(200);
        builder.Property(e => e.BrandName).HasMaxLength(200);
        builder.Property(e => e.Category).IsRequired().HasMaxLength(100);
        builder.Property(e => e.DosageForm).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Strength).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.SideEffects).HasMaxLength(2000);
        builder.Property(e => e.Contraindications).HasMaxLength(2000);
        builder.Property(e => e.UnitPrice).HasPrecision(18, 2);
        builder.Property(e => e.Manufacturer).HasMaxLength(200);
        builder.Property(e => e.BatchNumber).HasMaxLength(50);
        builder.Property(e => e.StorageConditions).HasMaxLength(500);

        builder.HasIndex(e => e.Name);
        builder.HasIndex(e => e.Category);
        builder.HasIndex(e => e.GenericName);
    }
}
