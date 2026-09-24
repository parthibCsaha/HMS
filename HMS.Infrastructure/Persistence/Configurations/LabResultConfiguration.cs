using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class LabResultConfiguration : IEntityTypeConfiguration<LabResult>
{
    public void Configure(EntityTypeBuilder<LabResult> builder)
    {
        builder.ToTable("lab_results");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Result).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Unit).HasMaxLength(50);
        builder.Property(e => e.ReferenceRange).HasMaxLength(200);
        builder.Property(e => e.Interpretation).HasMaxLength(1000);
        builder.Property(e => e.Notes).HasMaxLength(2000);
        builder.Property(e => e.AttachmentUrl).HasMaxLength(500);

        builder
            .HasOne(e => e.LabOrderItem)
            .WithOne(loi => loi.Result)
            .HasForeignKey<LabResult>(e => e.LabOrderItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.LabOrder)
            .WithMany()
            .HasForeignKey(e => e.LabOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Patient)
            .WithMany()
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.LabTest)
            .WithMany()
            .HasForeignKey(e => e.LabTestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
