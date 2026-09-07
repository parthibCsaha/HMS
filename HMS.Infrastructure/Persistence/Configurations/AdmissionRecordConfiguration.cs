using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class AdmissionRecordConfiguration : IEntityTypeConfiguration<AdmissionRecord>
{
    public void Configure(EntityTypeBuilder<AdmissionRecord> builder)
    {
        builder.ToTable("admission_records");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.AdmissionCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.ReasonForAdmission).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Diagnosis).HasMaxLength(500);
        builder.Property(e => e.DischargeSummary).HasMaxLength(2000);
        builder.Property(e => e.DischargeCondition).HasMaxLength(50);

        builder.HasIndex(e => e.AdmissionCode).IsUnique();
        builder.HasIndex(e => new { e.PatientId, e.IsActive });

        builder.HasOne(e => e.Patient)
            .WithMany(p => p.AdmissionRecords)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.AdmittingDoctor)
            .WithMany(d => d.AdmissionRecords)
            .HasForeignKey(e => e.AdmittingDoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Ward)
            .WithMany(w => w.AdmissionRecords)
            .HasForeignKey(e => e.WardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Bed)
            .WithMany(b => b.AdmissionRecords)
            .HasForeignKey(e => e.BedId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
