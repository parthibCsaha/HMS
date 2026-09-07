using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.DTOs;
using HMS.Application.Features.Beds.Services;
using MediatR;

namespace HMS.Application.Features.Beds.Queries;

public class GetBedsByWardQueryHandler(IBedService svc) : IRequestHandler<GetBedsByWardQuery, ApiResponse<IEnumerable<BedListItemDto>>>
{
    public async Task<ApiResponse<IEnumerable<BedListItemDto>>> Handle(GetBedsByWardQuery r, CancellationToken ct)
        => await svc.GetByWardAsync(r.WardId, ct);
}
