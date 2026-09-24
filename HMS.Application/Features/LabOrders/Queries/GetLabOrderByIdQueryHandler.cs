using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.DTOs;
using MediatR;

namespace HMS.Application.Features.LabOrders.Queries;

public class GetLabOrderByIdQueryHandler(ILabOrderRepository repo)
    : IRequestHandler<GetLabOrderByIdQuery, ApiResponse<LabOrderDetailDto>>
{
    public async Task<ApiResponse<LabOrderDetailDto>> Handle(
        GetLabOrderByIdQuery request,
        CancellationToken ct
    )
    {
        var labOrder =
            await repo.GetWithItemsAsync(request.Id, ct) ?? throw new NotFoundException("LabOrder", request.Id);

        var items = labOrder
            .Items.Select(item => new LabOrderItemDto(
                item.Id,
                item.LabTest?.Name ?? "",
                item.LabTest?.Code ?? "",
                item.Status.ToString(),
                item.Price,
                item.Result != null
                    ? new LabResultDto(
                        item.Result.Id,
                        item.Result.Result,
                        item.Result.Unit,
                        item.Result.ReferenceRange,
                        item.Result.Interpretation,
                        item.Result.ResultedAt,
                        item.Result.Notes,
                        item.Result.IsAbnormal ?? false
                    )
                    : null
            ))
            .ToList();

        var dto = new LabOrderDetailDto(
            labOrder.Id,
            labOrder.OrderCode,
            labOrder.PatientId,
            $"{labOrder.Patient.User.FirstName} {labOrder.Patient.User.LastName}",
            labOrder.OrderingDoctorId,
            $"{labOrder.OrderingDoctor.User.FirstName} {labOrder.OrderingDoctor.User.LastName}",
            labOrder.Status.ToString(),
            labOrder.OrderDate,
            labOrder.ClinicalNotes,
            labOrder.Priority,
            labOrder.IsBilled,
            items,
            labOrder.CreatedAt
        );

        return ApiResponse<LabOrderDetailDto>.Success(dto);
    }
}
