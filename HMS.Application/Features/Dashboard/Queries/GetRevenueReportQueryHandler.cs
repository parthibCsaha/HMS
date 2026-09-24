using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public class GetRevenueReportQueryHandler(IDashboardRepository repo)
    : IRequestHandler<GetRevenueReportQuery, ApiResponse<IEnumerable<RevenueSummaryDto>>>
{
    public async Task<ApiResponse<IEnumerable<RevenueSummaryDto>>> Handle(
        GetRevenueReportQuery r,
        CancellationToken ct
    ) =>
        ApiResponse<IEnumerable<RevenueSummaryDto>>.Success(
            await repo.GetRevenueReportAsync(r.From, r.To, ct)
        );
}
