using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.DTOs;
using HMS.Application.Features.Invoices.Services;
using MediatR;

namespace HMS.Application.Features.Invoices.Queries;

public class GetInvoiceByIdQueryHandler(IInvoiceService svc) : IRequestHandler<GetInvoiceByIdQuery, ApiResponse<InvoiceDetailDto>>
{
    public async Task<ApiResponse<InvoiceDetailDto>> Handle(GetInvoiceByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
