using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.DTOs;
using HMS.Application.Features.MedicalRecords.Services;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Queries;

public class GetMedicalRecordsByPatientQueryHandler(IMedicalRecordService svc) : IRequestHandler<GetMedicalRecordsByPatientQuery, ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>> Handle(GetMedicalRecordsByPatientQuery r, CancellationToken ct)
        => await svc.GetByPatientAsync(r.PatientId, new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize }, ct);
}
