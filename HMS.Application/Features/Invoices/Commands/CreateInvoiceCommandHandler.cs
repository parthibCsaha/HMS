using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.Services;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public class CreateInvoiceCommandHandler(IInvoiceService svc) : IRequestHandler<CreateInvoiceCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateInvoiceCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
