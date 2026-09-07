using HMS.Application.Common.Models;
using HMS.Application.Features.Invoices.DTOs;
using MediatR;

namespace HMS.Application.Features.Invoices.Queries;

public record GetInvoicesQuery(int PageNumber = 1, int PageSize = 10, Guid? PatientId = null) : IRequest<ApiResponse<PaginatedResponse<InvoiceListItemDto>>>;
