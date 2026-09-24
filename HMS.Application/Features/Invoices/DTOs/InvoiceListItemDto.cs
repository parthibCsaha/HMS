namespace HMS.Application.Features.Invoices.DTOs;

public record InvoiceListItemDto(
    Guid Id,
    string InvoiceNumber,
    string PatientName,
    string Status,
    decimal TotalAmount,
    decimal PaidAmount,
    decimal BalanceAmount,
    DateTime InvoiceDate,
    DateTime? DueDate
);
