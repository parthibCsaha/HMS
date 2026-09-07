using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class BedConfiguration : IEntityTypeConfiguration<Bed>
{
    public void Configure(EntityTypeBuilder<Bed> builder)
    {
        builder.ToTable("beds");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.BedNumber).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.Notes).HasMaxLength(500);

        builder.HasIndex(e => new { e.WardId, e.BedNumber }).IsUnique();

        builder.HasOne(e => e.Ward)
            .WithMany(w => w.Beds)
            .HasForeignKey(e => e.WardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CurrentPatient)
            .WithMany()
            .HasForeignKey(e => e.CurrentPatientId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
