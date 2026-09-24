using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_logs");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.UserEmail).IsRequired().HasMaxLength(256);
        builder.Property(e => e.UserRole).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Action).IsRequired().HasMaxLength(50);
        builder.Property(e => e.EntityName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.OldValues).HasColumnType("text");
        builder.Property(e => e.NewValues).HasColumnType("text");
        builder.Property(e => e.IpAddress).HasMaxLength(50);
        builder.Property(e => e.UserAgent).HasMaxLength(500);
        builder.Property(e => e.Endpoint).HasMaxLength(500);
        builder.Property(e => e.HttpMethod).HasMaxLength(10);
        builder.Property(e => e.ErrorMessage).HasMaxLength(2000);

        builder.HasIndex(e => new { e.EntityName, e.EntityId });
        builder.HasIndex(e => e.LoggedAt);
        builder.HasIndex(e => e.UserId);

        builder
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
