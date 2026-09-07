using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.DTOs;
using HMS.Application.Features.Invoices.Services;
using MediatR;

namespace HMS.Application.Features.Invoices.Queries;

public class GetInvoicesQueryHandler(IInvoiceService svc) : IRequestHandler<GetInvoicesQuery, ApiResponse<PaginatedResponse<InvoiceListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<InvoiceListItemDto>>> Handle(GetInvoicesQuery r, CancellationToken ct)
        => await svc.GetInvoicesAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize }, r.PatientId, ct);
}
