using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public class AddPaymentCommandHandler(
    IInvoiceRepository repo,
    IUnitOfWork uow
) : IRequestHandler<AddPaymentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(AddPaymentCommand cmd, CancellationToken ct)
    {
        var invoice = await repo.GetByIdAsync(cmd.InvoiceId, ct) ?? throw new NotFoundException("Invoice", cmd.InvoiceId);

        invoice.PaidAmount += cmd.Amount;
        invoice.BalanceAmount = invoice.TotalAmount - invoice.PaidAmount;
        invoice.PaymentMethod = Enum.Parse<HMS.Domain.Enums.PaymentMethod>(cmd.PaymentMethod);
        invoice.TransactionReference = cmd.TransactionReference;
        invoice.PaidAt = DateTime.UtcNow;
        invoice.Status =
            invoice.BalanceAmount <= 0
                ? HMS.Domain.Enums.InvoiceStatus.Paid
                : HMS.Domain.Enums.InvoiceStatus.PartiallyPaid;

        repo.Update(invoice);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Payment recorded.");
    }
}
