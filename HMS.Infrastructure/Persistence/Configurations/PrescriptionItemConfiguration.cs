using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
{
    public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
    {
        builder.ToTable("prescription_items");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Dosage).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Frequency).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Route).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Instructions).HasMaxLength(500);

        builder
            .HasOne(e => e.Prescription)
            .WithMany(p => p.Items)
            .HasForeignKey(e => e.PrescriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.Medication)
            .WithMany(m => m.PrescriptionItems)
            .HasForeignKey(e => e.MedicationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
