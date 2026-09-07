using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public record CreateInvoiceItemInput(string Description, string ItemType, int Quantity, decimal UnitPrice, decimal TaxPercent = 0, decimal DiscountPercent = 0);

public record CreateInvoiceCommand(Guid PatientId, Guid? AppointmentId, Guid? AdmissionId,
    decimal TaxPercent, decimal DiscountPercent, DateTime? DueDate, string? Notes,
    List<CreateInvoiceItemInput> Items) : IRequest<ApiResponse<Guid>>;
