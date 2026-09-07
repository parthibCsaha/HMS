using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class LabOrderItemConfiguration : IEntityTypeConfiguration<LabOrderItem>
{
    public void Configure(EntityTypeBuilder<LabOrderItem> builder)
    {
        builder.ToTable("lab_order_items");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.Price).HasPrecision(18, 2);

        builder.HasOne(e => e.LabOrder)
            .WithMany(lo => lo.Items)
            .HasForeignKey(e => e.LabOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.LabTest)
            .WithMany(lt => lt.LabOrderItems)
            .HasForeignKey(e => e.LabTestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
