using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class WardConfiguration : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> builder)
    {
        builder.ToTable("wards");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.WardNumber).IsRequired().HasMaxLength(20);
        builder.Property(e => e.WardType).IsRequired().HasConversion<string>().HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.ChargePerDay).HasPrecision(18, 2);
        builder.Property(e => e.Location).HasMaxLength(200);

        builder
            .HasOne(e => e.Department)
            .WithMany(d => d.Wards)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
