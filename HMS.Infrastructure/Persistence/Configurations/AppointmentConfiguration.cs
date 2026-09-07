using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointments");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.AppointmentCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.AppointmentDate).IsRequired();
        builder.Property(e => e.AppointmentTime).IsRequired();
        builder.Property(e => e.ChiefComplaint).HasMaxLength(500);
        builder.Property(e => e.Notes).HasMaxLength(2000);
        builder.Property(e => e.CancellationReason).HasMaxLength(500);
        builder.Property(e => e.ConsultationFee).HasPrecision(18, 2);
        builder.Property(e => e.Type).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(30);

        builder.HasIndex(e => e.AppointmentCode).IsUnique();
        builder.HasIndex(e => new { e.DoctorId, e.AppointmentDate });
        builder.HasIndex(e => e.PatientId);
        builder.HasIndex(e => e.Status);

        builder.HasOne(e => e.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Department)
            .WithMany(d => d.Appointments)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ReferredByDoctor)
            .WithMany()
            .HasForeignKey(e => e.ReferredByDoctorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
