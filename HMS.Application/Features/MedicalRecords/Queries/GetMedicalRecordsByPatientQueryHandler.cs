using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.DTOs;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Queries;

public class GetMedicalRecordsByPatientQueryHandler(IMedicalRecordRepository repo)
    : IRequestHandler<GetMedicalRecordsByPatientQuery, ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>> Handle(
        GetMedicalRecordsByPatientQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedByPatientAsync(
            request.PatientId,
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize },
            ct
        );

        var dtos = items.Select(record => new MedicalRecordListItemDto(
            record.Id,
            record.RecordCode,
            record.Doctor?.User != null
                ? $"{record.Doctor.User.FirstName} {record.Doctor.User.LastName}"
                : "",
            record.VisitDate,
            record.Diagnosis,
            record.ChiefComplaint,
            record.IsConfidential
        ));

        var response = PaginatedResponse<MedicalRecordListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>.Success(response);
    }
}
