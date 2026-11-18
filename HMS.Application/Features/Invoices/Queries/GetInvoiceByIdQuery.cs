using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.Invoices.DTOs;
using MediatR;

namespace HMS.Application.Features.Invoices.Queries;

[RequirePermission(Permissions.InvoicesView)]
public record GetInvoiceByIdQuery(Guid Id) : IRequest<ApiResponse<InvoiceDetailDto>>;
