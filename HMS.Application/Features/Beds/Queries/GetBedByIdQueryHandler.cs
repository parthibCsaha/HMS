using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.DTOs;
using HMS.Application.Features.Beds.Services;
using MediatR;

namespace HMS.Application.Features.Beds.Queries;

public class GetBedByIdQueryHandler(IBedService svc) : IRequestHandler<GetBedByIdQuery, ApiResponse<BedDetailDto>>
{
    public async Task<ApiResponse<BedDetailDto>> Handle(GetBedByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
