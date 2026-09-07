using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.PatientCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.DateOfBirth).IsRequired();
        builder.Property(e => e.Gender).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.BloodGroup).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.Address).IsRequired().HasMaxLength(500);
        builder.Property(e => e.City).IsRequired().HasMaxLength(100);
        builder.Property(e => e.State).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Country).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PostalCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.EmergencyContactName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.EmergencyContactPhone).IsRequired().HasMaxLength(20);
        builder.Property(e => e.EmergencyContactRelation).IsRequired().HasMaxLength(50);
        builder.Property(e => e.InsuranceProvider).HasMaxLength(200);
        builder.Property(e => e.InsurancePolicyNumber).HasMaxLength(100);
        builder.Property(e => e.Allergies).HasMaxLength(2000);
        builder.Property(e => e.ChronicConditions).HasMaxLength(2000);
        builder.Property(e => e.Notes).HasMaxLength(2000);

        builder.Ignore(e => e.Age);

        builder.HasIndex(e => e.PatientCode).IsUnique();
        builder.HasIndex(e => e.UserId).IsUnique();

        builder.HasOne(e => e.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
