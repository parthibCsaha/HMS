using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.LabOrders.DTOs;
using MediatR;

namespace HMS.Application.Features.LabOrders.Queries;

[RequirePermission(Permissions.LabOrdersView)]
public record GetLabOrderByIdQuery(Guid Id) : IRequest<ApiResponse<LabOrderDetailDto>>;
