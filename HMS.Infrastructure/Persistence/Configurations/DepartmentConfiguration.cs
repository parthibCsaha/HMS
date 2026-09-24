using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Code).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.Location).HasMaxLength(200);
        builder.Property(e => e.Phone).HasMaxLength(20);
        builder.Property(e => e.Email).HasMaxLength(256);

        builder.HasIndex(e => e.Code).IsUnique();
        builder.HasIndex(e => e.Name).IsUnique();

        builder
            .HasOne(e => e.HeadDoctor)
            .WithMany()
            .HasForeignKey(e => e.HeadDoctorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
