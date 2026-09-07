using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("prescriptions");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.PrescriptionCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Instructions).HasMaxLength(2000);
        builder.Property(e => e.Notes).HasMaxLength(2000);

        builder.HasIndex(e => e.PrescriptionCode).IsUnique();
        builder.HasIndex(e => e.PatientId);

        builder.HasOne(e => e.MedicalRecord)
            .WithMany(mr => mr.Prescriptions)
            .HasForeignKey(e => e.MedicalRecordId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
