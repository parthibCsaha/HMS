using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.DTOs;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public class GetPatientsQueryHandler(IPatientRepository patientRepository)
    : IRequestHandler<GetPatientsQuery, ApiResponse<PaginatedResponse<PatientListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<PatientListItemDto>>> Handle(
        GetPatientsQuery request,
        CancellationToken ct
    )
    {
        var (items, totalCount) = await patientRepository.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, SearchTerm = request.SearchTerm },
            ct
        );

        var dtos = items.Select(patient => new PatientListItemDto(
            patient.Id,
            patient.PatientCode,
            patient.User.FirstName,
            patient.User.LastName,
            patient.User.Email,
            patient.User.Phone,
            patient.DateOfBirth,
            patient.Age,
            patient.Gender.ToString(),
            patient.BloodGroup.ToString(),
            patient.City,
            patient.IsAdmitted
        ));

        var response = PaginatedResponse<PatientListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            totalCount
        );
        return ApiResponse<PaginatedResponse<PatientListItemDto>>.Success(response);
    }
}
