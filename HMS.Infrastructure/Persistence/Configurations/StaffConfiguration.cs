using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("staff");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.StaffCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.StaffType).IsRequired().HasConversion<string>().HasMaxLength(50);
        builder.Property(e => e.Qualification).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Shift).HasMaxLength(50);

        builder.HasIndex(e => e.StaffCode).IsUnique();
        builder.HasIndex(e => e.UserId).IsUnique();

        builder.HasOne(e => e.User)
            .WithOne(u => u.Staff)
            .HasForeignKey<Staff>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Department)
            .WithMany(d => d.Staff)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Ward)
            .WithMany(w => w.Staff)
            .HasForeignKey(e => e.WardId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
