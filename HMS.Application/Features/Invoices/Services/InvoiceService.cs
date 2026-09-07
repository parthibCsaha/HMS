using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.Commands;
using HMS.Application.Features.Invoices.DTOs;
using HMS.Domain.Entities;

namespace HMS.Application.Features.Invoices.Services;

public class InvoiceService(IInvoiceRepository repo, HMS.Application.Common.Interfaces.Services.ICodeGeneratorService codeGen, IUnitOfWork uow) : IInvoiceService
{
    public async Task<ApiResponse<PaginatedResponse<InvoiceListItemDto>>> GetInvoicesAsync(PaginationQuery q, Guid? patientId, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(q, patientId, null, ct);
        var dtos = items.Select(i => new InvoiceListItemDto(i.Id, i.InvoiceNumber,
            $"{i.Patient.User.FirstName} {i.Patient.User.LastName}", i.Status.ToString(),
            i.TotalAmount, i.PaidAmount, i.BalanceAmount, i.InvoiceDate, i.DueDate));
        return ApiResponse<PaginatedResponse<InvoiceListItemDto>>.Success(PaginatedResponse<InvoiceListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<InvoiceDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var i = await repo.GetWithItemsAsync(id, ct) ?? throw new NotFoundException("Invoice", id);
        var items = i.Items.Select(it => new InvoiceItemDto(it.Id, it.Description, it.ItemType, it.Quantity, it.UnitPrice, it.TotalPrice)).ToList();
        return ApiResponse<InvoiceDetailDto>.Success(new InvoiceDetailDto(i.Id, i.InvoiceNumber, i.PatientId,
            $"{i.Patient.User.FirstName} {i.Patient.User.LastName}", i.AppointmentId, i.AdmissionId,
            i.Status.ToString(), i.SubTotal, i.TaxPercent, i.TaxAmount, i.DiscountPercent, i.DiscountAmount,
            i.TotalAmount, i.PaidAmount, i.BalanceAmount, i.InvoiceDate, i.DueDate,
            i.PaymentMethod?.ToString(), i.TransactionReference, i.PaidAt, i.Notes,
            i.InsuranceClaimNumber, i.InsuranceCoveredAmount ?? 0m, items, i.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateInvoiceCommand cmd, CancellationToken ct)
    {
        var number = await codeGen.GenerateCodeAsync("INV", ct);
        var invoice = new Invoice
        {
            InvoiceNumber = number, PatientId = cmd.PatientId, AppointmentId = cmd.AppointmentId,
            AdmissionId = cmd.AdmissionId, Status = HMS.Domain.Enums.InvoiceStatus.Pending,
            TaxPercent = cmd.TaxPercent, DiscountPercent = cmd.DiscountPercent,
            InvoiceDate = DateTime.UtcNow, DueDate = cmd.DueDate, Notes = cmd.Notes
        };
        decimal subTotal = 0;
        foreach (var item in cmd.Items)
        {
            var total = item.Quantity * item.UnitPrice;
            var tax = total * (item.TaxPercent / 100m);
            var disc = total * (item.DiscountPercent / 100m);
            var lineTotal = total + tax - disc;
            invoice.Items.Add(new InvoiceItem
            {
                Description = item.Description, ItemType = item.ItemType, Quantity = item.Quantity,
                UnitPrice = item.UnitPrice, TaxPercent = item.TaxPercent, TaxAmount = tax,
                DiscountPercent = item.DiscountPercent, DiscountAmount = disc, TotalPrice = lineTotal
            });
            subTotal += lineTotal;
        }
        invoice.SubTotal = subTotal;
        invoice.TaxAmount = subTotal * (cmd.TaxPercent / 100m);
        invoice.DiscountAmount = subTotal * (cmd.DiscountPercent / 100m);
        invoice.TotalAmount = subTotal + invoice.TaxAmount - invoice.DiscountAmount;
        invoice.BalanceAmount = invoice.TotalAmount;
        await repo.AddAsync(invoice, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(invoice.Id, "Invoice created.");
    }

    public async Task<ApiResponse> AddPaymentAsync(Guid id, decimal amount, string paymentMethod, string? transRef, CancellationToken ct)
    {
        var invoice = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Invoice", id);
        invoice.PaidAmount += amount; invoice.BalanceAmount = invoice.TotalAmount - invoice.PaidAmount;
        invoice.PaymentMethod = Enum.Parse<HMS.Domain.Enums.PaymentMethod>(paymentMethod);
        invoice.TransactionReference = transRef; invoice.PaidAt = DateTime.UtcNow;
        invoice.Status = invoice.BalanceAmount <= 0 ? HMS.Domain.Enums.InvoiceStatus.Paid : HMS.Domain.Enums.InvoiceStatus.PartiallyPaid;
        repo.Update(invoice); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Payment recorded.");
    }

    public async Task<ApiResponse> CancelAsync(Guid id, CancellationToken ct)
    {
        var invoice = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Invoice", id);
        invoice.Status = HMS.Domain.Enums.InvoiceStatus.Cancelled;
        repo.Update(invoice); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Invoice cancelled.");
    }
}
