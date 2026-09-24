using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.DTOs;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public class GetPrescriptionsByPatientQueryHandler(IPrescriptionRepository repo)
    : IRequestHandler<GetPrescriptionsByPatientQuery, ApiResponse<PaginatedResponse<PrescriptionListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<PrescriptionListItemDto>>> Handle(
        GetPrescriptionsByPatientQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedByPatientAsync(
            request.PatientId,
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize },
            ct
        );

        var dtos = items.Select(prescription => new PrescriptionListItemDto(
            prescription.Id,
            prescription.PrescriptionCode,
            prescription.Doctor?.User != null
                ? $"{prescription.Doctor.User.FirstName} {prescription.Doctor.User.LastName}"
                : "",
            prescription.IssuedDate,
            prescription.IsDispensed,
            prescription.Items.Count
        ));

        var response = PaginatedResponse<PrescriptionListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<PrescriptionListItemDto>>.Success(response);
    }
}
