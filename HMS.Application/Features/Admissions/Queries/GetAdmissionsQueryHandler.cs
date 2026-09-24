using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.DTOs;
using MediatR;

namespace HMS.Application.Features.Admissions.Queries;

public class GetAdmissionsQueryHandler(IAdmissionRecordRepository repo)
    : IRequestHandler<GetAdmissionsQuery, ApiResponse<PaginatedResponse<AdmissionListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<AdmissionListItemDto>>> Handle(
        GetAdmissionsQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize },
            request.PatientId,
            request.IsActive,
            ct
        );

        var dtos = items.Select(admission => new AdmissionListItemDto(
            admission.Id,
            admission.AdmissionCode,
            $"{admission.Patient.User.FirstName} {admission.Patient.User.LastName}",
            $"{admission.AdmittingDoctor.User.FirstName} {admission.AdmittingDoctor.User.LastName}",
            admission.Ward.Name,
            admission.Bed.BedNumber,
            admission.AdmissionDate,
            admission.IsActive
        ));

        var response = PaginatedResponse<AdmissionListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<AdmissionListItemDto>>.Success(response);
    }
}
