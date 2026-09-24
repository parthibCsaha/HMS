using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public class GetPatientDemographicsQueryHandler(IDashboardRepository repo)
    : IRequestHandler<GetPatientDemographicsQuery, ApiResponse<PatientDemographicsDto>>
{
    public async Task<ApiResponse<PatientDemographicsDto>> Handle(
        GetPatientDemographicsQuery r,
        CancellationToken ct
    ) => ApiResponse<PatientDemographicsDto>.Success(await repo.GetPatientDemographicsAsync(ct));
}
