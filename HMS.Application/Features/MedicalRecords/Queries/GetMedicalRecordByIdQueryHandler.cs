using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.DTOs;
using HMS.Application.Features.MedicalRecords.Services;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Queries;

public class GetMedicalRecordByIdQueryHandler(IMedicalRecordService svc) : IRequestHandler<GetMedicalRecordByIdQuery, ApiResponse<MedicalRecordDetailDto>>
{
    public async Task<ApiResponse<MedicalRecordDetailDto>> Handle(GetMedicalRecordByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
