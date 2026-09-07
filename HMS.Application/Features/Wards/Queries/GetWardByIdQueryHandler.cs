using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.DTOs;
using HMS.Application.Features.Wards.Services;
using MediatR;

namespace HMS.Application.Features.Wards.Queries;

public class GetWardByIdQueryHandler(IWardService svc) : IRequestHandler<GetWardByIdQuery, ApiResponse<WardDetailDto>>
{
    public async Task<ApiResponse<WardDetailDto>> Handle(GetWardByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
