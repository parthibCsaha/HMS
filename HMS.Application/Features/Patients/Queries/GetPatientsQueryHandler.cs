using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.DTOs;
using HMS.Application.Features.Patients.Services;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public class GetPatientsQueryHandler(IPatientService patientService) : IRequestHandler<GetPatientsQuery, ApiResponse<PaginatedResponse<PatientListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<PatientListItemDto>>> Handle(GetPatientsQuery request, CancellationToken ct)
    {
        var query = new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, SearchTerm = request.SearchTerm, SortBy = request.SortBy, IsDescending = request.IsDescending };
        return await patientService.GetPatientsAsync(query, ct);
    }
}
