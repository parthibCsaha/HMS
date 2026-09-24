using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public class CancelInvoiceCommandHandler(
    IInvoiceRepository repo,
    IUnitOfWork uow
) : IRequestHandler<CancelInvoiceCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CancelInvoiceCommand cmd, CancellationToken ct)
    {
        var invoice = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Invoice", cmd.Id);

        invoice.Status = HMS.Domain.Enums.InvoiceStatus.Cancelled;

        repo.Update(invoice);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Invoice cancelled.");
    }
}
