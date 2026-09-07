using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.Services;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public class AddPaymentCommandHandler(IInvoiceService svc) : IRequestHandler<AddPaymentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(AddPaymentCommand r, CancellationToken ct)
        => await svc.AddPaymentAsync(r.InvoiceId, r.Amount, r.PaymentMethod, r.TransactionReference, ct);
}
