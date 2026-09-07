using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class LabTestConfiguration : IEntityTypeConfiguration<LabTest>
{
    public void Configure(EntityTypeBuilder<LabTest> builder)
    {
        builder.ToTable("lab_tests");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Code).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Category).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Price).HasPrecision(18, 2);
        builder.Property(e => e.SampleType).HasMaxLength(50);
        builder.Property(e => e.PreparationInstructions).HasMaxLength(1000);
        builder.Property(e => e.ReferenceRange).HasMaxLength(500);
        builder.Property(e => e.Unit).HasMaxLength(50);

        builder.HasIndex(e => e.Code).IsUnique();
        builder.HasIndex(e => e.Category);
    }
}
