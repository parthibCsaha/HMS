using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public class GetAppointmentTrendsQueryHandler(IDashboardRepository repo)
    : IRequestHandler<GetAppointmentTrendsQuery, ApiResponse<IEnumerable<AppointmentTrendDto>>>
{
    public async Task<ApiResponse<IEnumerable<AppointmentTrendDto>>> Handle(
        GetAppointmentTrendsQuery r,
        CancellationToken ct
    ) =>
        ApiResponse<IEnumerable<AppointmentTrendDto>>.Success(
            await repo.GetAppointmentTrendsAsync(r.From, r.To, ct)
        );
}
