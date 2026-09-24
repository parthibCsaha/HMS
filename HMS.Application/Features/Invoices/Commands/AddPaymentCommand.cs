using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public record AddPaymentCommand(
    Guid InvoiceId,
    decimal Amount,
    string PaymentMethod,
    string? TransactionReference
) : IRequest<ApiResponse>;
