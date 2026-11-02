using HMS.Domain.Common;
using HMS.Domain.Enums;

namespace HMS.Domain.Entities;

public class Invoice : BaseEntity
{
    public string InvoiceNumber { get; set; } = string.Empty; // INV-00001
    public Guid PatientId { get; set; }
    public Guid? AppointmentId { get; set; }
    public Guid? AdmissionId { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
    public decimal SubTotal { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal BalanceAmount { get; set; }
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? TransactionReference { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? Notes { get; set; }
    public string? InsuranceClaimNumber { get; set; }
    public decimal? InsuranceCoveredAmount { get; set; }

    // Navigation Properties
    public Patient Patient { get; set; } = null!;
    public Appointment? Appointment { get; set; }
    public AdmissionRecord? Admission { get; set; }
    public ICollection<InvoiceItem> Items { get; set; } = [];

    // ──── Domain Methods ────

    /// <summary>
    /// Recalculates TaxAmount, DiscountAmount, TotalAmount, and BalanceAmount
    /// from SubTotal, TaxPercent, DiscountPercent, and PaidAmount.
    /// </summary>
    public void Recalculate()
    {
        TaxAmount = SubTotal * (TaxPercent / 100m);
        DiscountAmount = SubTotal * (DiscountPercent / 100m);
        TotalAmount = SubTotal + TaxAmount - DiscountAmount;
        BalanceAmount = TotalAmount - PaidAmount - (InsuranceCoveredAmount ?? 0m);
    }

    /// <summary>
    /// Records a payment against this invoice.
    /// </summary>
    public void RecordPayment(decimal amount, PaymentMethod method, string? transactionRef = null)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Payment amount must be positive.");

        PaidAmount += amount;
        BalanceAmount = TotalAmount - PaidAmount - (InsuranceCoveredAmount ?? 0m);
        PaymentMethod = method;
        TransactionReference = transactionRef;

        if (BalanceAmount <= 0m)
        {
            Status = InvoiceStatus.Paid;
            PaidAt = DateTime.UtcNow;
            BalanceAmount = 0m; // Prevent negative balance
        }
        else
        {
            Status = InvoiceStatus.PartiallyPaid;
        }
    }

    /// <summary>
    /// Applies insurance coverage to this invoice.
    /// </summary>
    public void ApplyInsuranceCoverage(decimal coveredAmount, string claimNumber)
    {
        InsuranceCoveredAmount = coveredAmount;
        InsuranceClaimNumber = claimNumber;
        Recalculate();
    }

    /// <summary>
    /// Cancels this invoice. Only pending or partially paid invoices can be cancelled.
    /// </summary>
    public void Cancel()
    {
        if (Status == InvoiceStatus.Paid)
            throw new InvalidOperationException("Cannot cancel a fully paid invoice.");

        Status = InvoiceStatus.Cancelled;
    }
}
