using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.DTOs;
using HMS.Application.Features.LabOrders.Services;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.LabOrders.Queries;

public class GetLabOrdersQueryHandler(ILabOrderService svc) : IRequestHandler<GetLabOrdersQuery, ApiResponse<PaginatedResponse<LabOrderListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<LabOrderListItemDto>>> Handle(GetLabOrdersQuery r, CancellationToken ct)
        => await svc.GetLabOrdersAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize }, r.PatientId, r.Status, ct);
}
