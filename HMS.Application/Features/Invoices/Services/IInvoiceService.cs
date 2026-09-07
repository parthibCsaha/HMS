using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.Commands;
using HMS.Application.Features.Invoices.DTOs;

namespace HMS.Application.Features.Invoices.Services;

public interface IInvoiceService
{
    Task<ApiResponse<PaginatedResponse<InvoiceListItemDto>>> GetInvoicesAsync(PaginationQuery q, Guid? patientId, CancellationToken ct);
    Task<ApiResponse<InvoiceDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateInvoiceCommand cmd, CancellationToken ct);
    Task<ApiResponse> AddPaymentAsync(Guid id, decimal amount, string paymentMethod, string? transactionRef, CancellationToken ct);
    Task<ApiResponse> CancelAsync(Guid id, CancellationToken ct);
}
