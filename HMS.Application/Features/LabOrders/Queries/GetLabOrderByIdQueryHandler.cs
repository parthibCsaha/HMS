using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.DTOs;
using HMS.Application.Features.LabOrders.Services;
using MediatR;

namespace HMS.Application.Features.LabOrders.Queries;

public class GetLabOrderByIdQueryHandler(ILabOrderService svc) : IRequestHandler<GetLabOrderByIdQuery, ApiResponse<LabOrderDetailDto>>
{
    public async Task<ApiResponse<LabOrderDetailDto>> Handle(GetLabOrderByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
