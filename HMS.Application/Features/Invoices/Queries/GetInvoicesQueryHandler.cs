using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.DTOs;
using MediatR;

namespace HMS.Application.Features.Invoices.Queries;

public class GetInvoicesQueryHandler(IInvoiceRepository repo)
    : IRequestHandler<GetInvoicesQuery, ApiResponse<PaginatedResponse<InvoiceListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<InvoiceListItemDto>>> Handle(
        GetInvoicesQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize },
            request.PatientId,
            null,
            ct
        );

        var dtos = items.Select(invoice => new InvoiceListItemDto(
            invoice.Id,
            invoice.InvoiceNumber,
            $"{invoice.Patient.User.FirstName} {invoice.Patient.User.LastName}",
            invoice.Status.ToString(),
            invoice.TotalAmount,
            invoice.PaidAmount,
            invoice.BalanceAmount,
            invoice.InvoiceDate,
            invoice.DueDate
        ));

        var response = PaginatedResponse<InvoiceListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<InvoiceListItemDto>>.Success(response);
    }
}
