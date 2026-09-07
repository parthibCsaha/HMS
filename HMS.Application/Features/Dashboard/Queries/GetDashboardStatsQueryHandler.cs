using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public class GetDashboardStatsQueryHandler(IDashboardRepository repo) : IRequestHandler<GetDashboardStatsQuery, ApiResponse<DashboardStatsDto>>
{
    public async Task<ApiResponse<DashboardStatsDto>> Handle(GetDashboardStatsQuery r, CancellationToken ct)
        => ApiResponse<DashboardStatsDto>.Success(await repo.GetDashboardStatsAsync(ct));
}
