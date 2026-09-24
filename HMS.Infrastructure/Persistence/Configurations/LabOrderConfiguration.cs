using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class LabOrderConfiguration : IEntityTypeConfiguration<LabOrder>
{
    public void Configure(EntityTypeBuilder<LabOrder> builder)
    {
        builder.ToTable("lab_orders");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.OrderCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.ClinicalNotes).HasMaxLength(2000);
        builder.Property(e => e.Priority).IsRequired().HasMaxLength(20);

        builder.HasIndex(e => e.OrderCode).IsUnique();
        builder.HasIndex(e => e.PatientId);
        builder.HasIndex(e => e.Status);

        builder
            .HasOne(e => e.Patient)
            .WithMany(p => p.LabOrders)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.OrderingDoctor)
            .WithMany(d => d.LabOrders)
            .HasForeignKey(e => e.OrderingDoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.MedicalRecord)
            .WithMany(mr => mr.LabOrders)
            .HasForeignKey(e => e.MedicalRecordId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(e => e.Admission)
            .WithMany()
            .HasForeignKey(e => e.AdmissionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
