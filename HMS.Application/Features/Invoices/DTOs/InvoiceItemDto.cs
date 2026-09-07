namespace HMS.Application.Features.Invoices.DTOs;

public record InvoiceItemDto(Guid Id, string Description, string ItemType, int Quantity,
    decimal UnitPrice, decimal TotalPrice);
