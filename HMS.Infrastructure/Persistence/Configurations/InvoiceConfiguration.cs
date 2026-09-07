using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.SubTotal).HasPrecision(18, 2);
        builder.Property(e => e.TaxPercent).HasPrecision(5, 2);
        builder.Property(e => e.TaxAmount).HasPrecision(18, 2);
        builder.Property(e => e.DiscountPercent).HasPrecision(5, 2);
        builder.Property(e => e.DiscountAmount).HasPrecision(18, 2);
        builder.Property(e => e.TotalAmount).HasPrecision(18, 2);
        builder.Property(e => e.PaidAmount).HasPrecision(18, 2);
        builder.Property(e => e.BalanceAmount).HasPrecision(18, 2);
        builder.Property(e => e.PaymentMethod).HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.TransactionReference).HasMaxLength(200);
        builder.Property(e => e.Notes).HasMaxLength(2000);
        builder.Property(e => e.InsuranceClaimNumber).HasMaxLength(100);
        builder.Property(e => e.InsuranceCoveredAmount).HasPrecision(18, 2);

        builder.HasIndex(e => e.InvoiceNumber).IsUnique();
        builder.HasIndex(e => e.PatientId);
        builder.HasIndex(e => e.Status);

        builder.HasOne(e => e.Patient)
            .WithMany(p => p.Invoices)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Appointment)
            .WithMany()
            .HasForeignKey(e => e.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Admission)
            .WithMany()
            .HasForeignKey(e => e.AdmissionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
