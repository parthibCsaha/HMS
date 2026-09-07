using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.DTOs;
using HMS.Application.Features.Patients.Services;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public class GetPatientByIdQueryHandler(IPatientService patientService) : IRequestHandler<GetPatientByIdQuery, ApiResponse<PatientDetailDto>>
{
    public async Task<ApiResponse<PatientDetailDto>> Handle(GetPatientByIdQuery request, CancellationToken ct)
    {
        return await patientService.GetPatientByIdAsync(request.Id, ct);
    }
}
