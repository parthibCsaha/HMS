using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public class CreateInvoiceCommandHandler(
    IInvoiceRepository repo,
    HMS.Application.Common.Interfaces.Services.ICodeGeneratorService codeGen,
    IUnitOfWork uow
) : IRequestHandler<CreateInvoiceCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateInvoiceCommand cmd, CancellationToken ct)
    {
        var number = await codeGen.GenerateCodeAsync("INV", ct);
        var invoice = new Invoice
        {
            InvoiceNumber = number,
            PatientId = cmd.PatientId,
            AppointmentId = cmd.AppointmentId,
            AdmissionId = cmd.AdmissionId,
            Status = HMS.Domain.Enums.InvoiceStatus.Pending,
            TaxPercent = cmd.TaxPercent,
            DiscountPercent = cmd.DiscountPercent,
            InvoiceDate = DateTime.UtcNow,
            DueDate = cmd.DueDate,
            Notes = cmd.Notes,
        };
        decimal subTotal = 0;
        foreach (var item in cmd.Items)
        {
            var total = item.Quantity * item.UnitPrice;
            var tax = total * (item.TaxPercent / 100m);
            var disc = total * (item.DiscountPercent / 100m);
            var lineTotal = total + tax - disc;
            invoice.Items.Add(
                new InvoiceItem
                {
                    Description = item.Description,
                    ItemType = item.ItemType,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TaxPercent = item.TaxPercent,
                    TaxAmount = tax,
                    DiscountPercent = item.DiscountPercent,
                    DiscountAmount = disc,
                    TotalPrice = lineTotal,
                }
            );
            subTotal += lineTotal;
        }
        invoice.SubTotal = subTotal;
        invoice.TaxAmount = subTotal * (cmd.TaxPercent / 100m);
        invoice.DiscountAmount = subTotal * (cmd.DiscountPercent / 100m);
        invoice.TotalAmount = subTotal + invoice.TaxAmount - invoice.DiscountAmount;
        invoice.BalanceAmount = invoice.TotalAmount;

        await repo.AddAsync(invoice, ct);
        await uow.SaveChangesAsync(ct);

        return ApiResponse<Guid>.Success(invoice.Id, "Invoice created.");
    }
}
