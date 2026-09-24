using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.DoctorCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Specialization).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Qualification).IsRequired().HasMaxLength(500);
        builder.Property(e => e.LicenseNumber).IsRequired().HasMaxLength(50);
        builder.Property(e => e.ConsultationFee).HasPrecision(18, 2);
        builder.Property(e => e.Biography).HasMaxLength(2000);
        builder.Property(e => e.AvailableDays).HasMaxLength(500);

        builder.HasIndex(e => e.DoctorCode).IsUnique();
        builder.HasIndex(e => e.LicenseNumber).IsUnique();
        builder.HasIndex(e => e.UserId).IsUnique();

        builder
            .HasOne(e => e.User)
            .WithOne(u => u.Doctor)
            .HasForeignKey<Doctor>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Department)
            .WithMany(d => d.Doctors)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
