using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Invoices.Commands;

public record CancelInvoiceCommand(Guid Id) : IRequest<ApiResponse>;
