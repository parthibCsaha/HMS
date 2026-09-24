using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.DTOs;
using MediatR;

namespace HMS.Application.Features.Invoices.Queries;

public class GetInvoiceByIdQueryHandler(IInvoiceRepository repo)
    : IRequestHandler<GetInvoiceByIdQuery, ApiResponse<InvoiceDetailDto>>
{
    public async Task<ApiResponse<InvoiceDetailDto>> Handle(
        GetInvoiceByIdQuery request,
        CancellationToken ct
    )
    {
        var invoice =
            await repo.GetWithItemsAsync(request.Id, ct) ?? throw new NotFoundException("Invoice", request.Id);

        var items = invoice
            .Items.Select(item => new InvoiceItemDto(
                item.Id,
                item.Description,
                item.ItemType,
                item.Quantity,
                item.UnitPrice,
                item.TotalPrice
            ))
            .ToList();

        var dto = new InvoiceDetailDto(
            invoice.Id,
            invoice.InvoiceNumber,
            invoice.PatientId,
            $"{invoice.Patient.User.FirstName} {invoice.Patient.User.LastName}",
            invoice.AppointmentId,
            invoice.AdmissionId,
            invoice.Status.ToString(),
            invoice.SubTotal,
            invoice.TaxPercent,
            invoice.TaxAmount,
            invoice.DiscountPercent,
            invoice.DiscountAmount,
            invoice.TotalAmount,
            invoice.PaidAmount,
            invoice.BalanceAmount,
            invoice.InvoiceDate,
            invoice.DueDate,
            invoice.PaymentMethod?.ToString(),
            invoice.TransactionReference,
            invoice.PaidAt,
            invoice.Notes,
            invoice.InsuranceClaimNumber,
            invoice.InsuranceCoveredAmount ?? 0m,
            items,
            invoice.CreatedAt
        );

        return ApiResponse<InvoiceDetailDto>.Success(dto);
    }
}
