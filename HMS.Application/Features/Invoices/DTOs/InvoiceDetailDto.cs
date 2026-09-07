namespace HMS.Application.Features.Invoices.DTOs;

public record InvoiceDetailDto(Guid Id, string InvoiceNumber, Guid PatientId, string PatientName,
    Guid? AppointmentId, Guid? AdmissionId, string Status, decimal SubTotal,
    decimal TaxPercent, decimal TaxAmount, decimal DiscountPercent, decimal DiscountAmount,
    decimal TotalAmount, decimal PaidAmount, decimal BalanceAmount, DateTime InvoiceDate,
    DateTime? DueDate, string? PaymentMethod, string? TransactionReference, DateTime? PaidAt,
    string? Notes, string? InsuranceClaimNumber, decimal InsuranceCoveredAmount,
    List<InvoiceItemDto> Items, DateTime CreatedAt);
