using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class VitalSignsConfiguration : IEntityTypeConfiguration<VitalSigns>
{
    public void Configure(EntityTypeBuilder<VitalSigns> builder)
    {
        builder.ToTable("vital_signs");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.TemperatureCelsius).HasPrecision(5, 2);
        builder.Property(e => e.OxygenSaturationPercent).HasPrecision(5, 2);
        builder.Property(e => e.WeightKg).HasPrecision(6, 2);
        builder.Property(e => e.HeightCm).HasPrecision(6, 2);
        builder.Property(e => e.BmiValue).HasPrecision(5, 2);
        builder.Property(e => e.BloodGlucoseMgDl).HasPrecision(7, 2);
        builder.Property(e => e.BloodPressure).HasMaxLength(20);
        builder.Property(e => e.PainLevel).HasMaxLength(10);
        builder.Property(e => e.Notes).HasMaxLength(2000);

        builder.HasIndex(e => new { e.PatientId, e.RecordedAt });

        builder
            .HasOne(e => e.Patient)
            .WithMany(p => p.VitalSigns)
            .HasForeignKey(e => e.PatientId)
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
