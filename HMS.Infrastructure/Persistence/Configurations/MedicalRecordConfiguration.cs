using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("medical_records");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.RecordCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.ChiefComplaint).IsRequired().HasMaxLength(500);
        builder.Property(e => e.PresentIllnessHistory).HasMaxLength(2000);
        builder.Property(e => e.PastMedicalHistory).HasMaxLength(2000);
        builder.Property(e => e.FamilyHistory).HasMaxLength(2000);
        builder.Property(e => e.SocialHistory).HasMaxLength(2000);
        builder.Property(e => e.ReviewOfSystems).HasMaxLength(2000);
        builder.Property(e => e.PhysicalExamination).HasMaxLength(2000);
        builder.Property(e => e.Diagnosis).IsRequired().HasMaxLength(500);
        builder.Property(e => e.DifferentialDiagnosis).HasMaxLength(500);
        builder.Property(e => e.Treatment).HasMaxLength(2000);
        builder.Property(e => e.Procedures).HasMaxLength(2000);
        builder.Property(e => e.Notes).HasMaxLength(2000);
        builder.Property(e => e.FollowUpInstructions).HasMaxLength(2000);

        builder.HasIndex(e => e.RecordCode).IsUnique();
        builder.HasIndex(e => e.PatientId);

        builder
            .HasOne(e => e.Patient)
            .WithMany(p => p.MedicalRecords)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Doctor)
            .WithMany(d => d.MedicalRecords)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Appointment)
            .WithMany()
            .HasForeignKey(e => e.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(e => e.Admission)
            .WithMany()
            .HasForeignKey(e => e.AdmissionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
