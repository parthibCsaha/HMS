using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.DTOs;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.LabOrders.Queries;

public record GetLabOrdersQuery(
    int PageNumber = 1,
    int PageSize = 10,
    Guid? PatientId = null,
    LabTestStatus? Status = null
) : IRequest<ApiResponse<PaginatedResponse<LabOrderListItemDto>>>;
