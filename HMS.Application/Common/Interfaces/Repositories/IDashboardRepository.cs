using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IDashboardRepository
{
    Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken ct = default);
    Task<IEnumerable<OccupancyReportDto>> GetOccupancyReportAsync(CancellationToken ct = default);
    Task<IEnumerable<AppointmentTrendDto>> GetAppointmentTrendsAsync(
        DateTime from,
        DateTime to,
        CancellationToken ct = default
    );
    Task<IEnumerable<RevenueSummaryDto>> GetRevenueReportAsync(
        DateTime from,
        DateTime to,
        CancellationToken ct = default
    );
    Task<PatientDemographicsDto> GetPatientDemographicsAsync(CancellationToken ct = default);
}
