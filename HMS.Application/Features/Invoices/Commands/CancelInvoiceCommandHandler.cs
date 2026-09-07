using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.Services;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public class CancelInvoiceCommandHandler(IInvoiceService svc) : IRequestHandler<CancelInvoiceCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CancelInvoiceCommand r, CancellationToken ct)
        => await svc.CancelAsync(r.Id, ct);
}
