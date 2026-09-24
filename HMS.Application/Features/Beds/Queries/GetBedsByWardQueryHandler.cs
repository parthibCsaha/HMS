using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.DTOs;
using MediatR;

namespace HMS.Application.Features.Beds.Queries;

public class GetBedsByWardQueryHandler(IBedRepository repo)
    : IRequestHandler<GetBedsByWardQuery, ApiResponse<IEnumerable<BedListItemDto>>>
{
    public async Task<ApiResponse<IEnumerable<BedListItemDto>>> Handle(
        GetBedsByWardQuery request,
        CancellationToken ct
    )
    {
        var beds = await repo.GetByWardAsync(request.WardId, ct);

        var dtos = beds.Select(bed => new BedListItemDto(
            bed.Id,
            bed.BedNumber,
            bed.Ward?.Name ?? "",
            bed.Status.ToString(),
            bed.CurrentPatient?.User != null
                ? $"{bed.CurrentPatient.User.FirstName} {bed.CurrentPatient.User.LastName}"
                : null
        ));

        return ApiResponse<IEnumerable<BedListItemDto>>.Success(dtos);
    }
}
