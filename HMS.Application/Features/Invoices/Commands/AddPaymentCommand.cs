using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

[RequirePermission(Permissions.InvoicesAddPayment)]
public record AddPaymentCommand(
    Guid InvoiceId,
    decimal Amount,
    string PaymentMethod,
    string? TransactionReference
) : IRequest<ApiResponse>;
