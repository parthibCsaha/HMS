using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.DTOs;
using MediatR;

namespace HMS.Application.Features.LabOrders.Queries;

public record GetLabOrderByIdQuery(Guid Id) : IRequest<ApiResponse<LabOrderDetailDto>>;
