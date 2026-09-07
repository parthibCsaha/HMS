using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.DTOs;
using HMS.Application.Features.Admissions.Services;
using MediatR;

namespace HMS.Application.Features.Admissions.Queries;

public class GetAdmissionsQueryHandler(IAdmissionService svc) : IRequestHandler<GetAdmissionsQuery, ApiResponse<PaginatedResponse<AdmissionListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<AdmissionListItemDto>>> Handle(GetAdmissionsQuery r, CancellationToken ct)
        => await svc.GetAdmissionsAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize }, r.PatientId, r.IsActive, ct);
}
