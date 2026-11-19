using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

[RequirePermission(Permissions.InvoicesCancel)]
public record CancelInvoiceCommand(Guid Id) : IRequest<ApiResponse>;
