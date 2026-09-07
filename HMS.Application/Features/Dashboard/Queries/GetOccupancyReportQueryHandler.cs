using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public class GetOccupancyReportQueryHandler(IDashboardRepository repo) : IRequestHandler<GetOccupancyReportQuery, ApiResponse<IEnumerable<OccupancyReportDto>>>
{
    public async Task<ApiResponse<IEnumerable<OccupancyReportDto>>> Handle(GetOccupancyReportQuery r, CancellationToken ct)
        => ApiResponse<IEnumerable<OccupancyReportDto>>.Success(await repo.GetOccupancyReportAsync(ct));
}
