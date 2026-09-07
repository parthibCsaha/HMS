using System.Data;
using Dapper;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;

namespace HMS.Infrastructure.Persistence.Repositories;

public class DashboardRepository(IDapperContext dapperContext) : IDashboardRepository
{
    public async Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken ct = default)
    {
        using var connection = dapperContext.CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<DashboardStatsDto>(
            "SELECT * FROM sp_get_dashboard_stats()");
        return result ?? new DashboardStatsDto(0, 0, 0, 0, 0, 0m, 0m);
    }

    public async Task<IEnumerable<OccupancyReportDto>> GetOccupancyReportAsync(CancellationToken ct = default)
    {
        using var connection = dapperContext.CreateConnection();
        return await connection.QueryAsync<OccupancyReportDto>("SELECT * FROM sp_get_occupancy_report()");
    }

    public async Task<IEnumerable<AppointmentTrendDto>> GetAppointmentTrendsAsync(DateTime from, DateTime to, CancellationToken ct = default)
    {
        using var connection = dapperContext.CreateConnection();
        return await connection.QueryAsync<AppointmentTrendDto>(
            "SELECT * FROM sp_get_appointment_trends(@From, @To)", new { From = from, To = to });
    }

    public async Task<IEnumerable<RevenueSummaryDto>> GetRevenueReportAsync(DateTime from, DateTime to, CancellationToken ct = default)
    {
        using var connection = dapperContext.CreateConnection();
        return await connection.QueryAsync<RevenueSummaryDto>(
            "SELECT * FROM sp_get_revenue_report(@From, @To)", new { From = from, To = to });
    }

    public async Task<PatientDemographicsDto> GetPatientDemographicsAsync(CancellationToken ct = default)
    {
        using var connection = dapperContext.CreateConnection();
        using var multi = await connection.QueryMultipleAsync("SELECT * FROM sp_get_patient_demographics()");
        var ageGroups = (await multi.ReadAsync<AgeGroupDto>()).ToList();
        var genderDist = (await multi.ReadAsync<GenderDistributionDto>()).ToList();
        var bloodDist = (await multi.ReadAsync<BloodGroupDistributionDto>()).ToList();
        return new PatientDemographicsDto(ageGroups, genderDist, bloodDist);
    }
}
