using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.DTOs;
using MediatR;

namespace HMS.Application.Features.Beds.Queries;

public class GetBedByIdQueryHandler(IBedRepository repo)
    : IRequestHandler<GetBedByIdQuery, ApiResponse<BedDetailDto>>
{
    public async Task<ApiResponse<BedDetailDto>> Handle(
        GetBedByIdQuery request,
        CancellationToken ct
    )
    {
        var bed = await repo.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("Bed", request.Id);

        var dto = new BedDetailDto(
            bed.Id,
            bed.BedNumber,
            bed.WardId,
            bed.Ward?.Name ?? "",
            bed.Status.ToString(),
            bed.CurrentPatientId,
            null,
            bed.OccupiedAt,
            bed.Notes,
            bed.CreatedAt
        );

        return ApiResponse<BedDetailDto>.Success(dto);
    }
}
