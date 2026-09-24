using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.DTOs;
using MediatR;

namespace HMS.Application.Features.Wards.Queries;

public class GetWardByIdQueryHandler(IWardRepository repo)
    : IRequestHandler<GetWardByIdQuery, ApiResponse<WardDetailDto>>
{
    public async Task<ApiResponse<WardDetailDto>> Handle(
        GetWardByIdQuery request,
        CancellationToken ct
    )
    {
        var ward = await repo.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("Ward", request.Id);

        var dto = new WardDetailDto(
            ward.Id,
            ward.Name,
            ward.WardNumber,
            ward.WardType.ToString(),
            ward.DepartmentId,
            ward.Department?.Name ?? "",
            ward.TotalBeds,
            ward.AvailableBeds,
            ward.Description,
            ward.ChargePerDay ?? 0m,
            ward.Location,
            ward.IsActive,
            ward.CreatedAt
        );

        return ApiResponse<WardDetailDto>.Success(dto);
    }
}
