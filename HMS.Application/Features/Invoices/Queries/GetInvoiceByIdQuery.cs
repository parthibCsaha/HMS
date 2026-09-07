using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.DTOs;
using MediatR;

namespace HMS.Application.Features.Invoices.Queries;

public record GetInvoiceByIdQuery(Guid Id) : IRequest<ApiResponse<InvoiceDetailDto>>;
