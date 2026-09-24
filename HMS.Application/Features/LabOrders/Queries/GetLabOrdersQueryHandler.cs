using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.DTOs;
using MediatR;

namespace HMS.Application.Features.LabOrders.Queries;

public class GetLabOrdersQueryHandler(ILabOrderRepository repo)
    : IRequestHandler<GetLabOrdersQuery, ApiResponse<PaginatedResponse<LabOrderListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<LabOrderListItemDto>>> Handle(
        GetLabOrdersQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize },
            request.PatientId,
            request.Status,
            ct
        );

        var dtos = items.Select(labOrder => new LabOrderListItemDto(
            labOrder.Id,
            labOrder.OrderCode,
            $"{labOrder.Patient.User.FirstName} {labOrder.Patient.User.LastName}",
            $"{labOrder.OrderingDoctor.User.FirstName} {labOrder.OrderingDoctor.User.LastName}",
            labOrder.Status.ToString(),
            labOrder.OrderDate,
            labOrder.Items.Count
        ));

        var response = PaginatedResponse<LabOrderListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<LabOrderListItemDto>>.Success(response);
    }
}
