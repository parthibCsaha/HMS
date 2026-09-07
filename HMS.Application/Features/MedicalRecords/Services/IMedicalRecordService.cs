using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.Commands;
using HMS.Application.Features.MedicalRecords.DTOs;

namespace HMS.Application.Features.MedicalRecords.Services;

public interface IMedicalRecordService
{
    Task<ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>> GetByPatientAsync(Guid patientId, PaginationQuery q, CancellationToken ct);
    Task<ApiResponse<MedicalRecordDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateMedicalRecordCommand cmd, CancellationToken ct);
    Task<ApiResponse> UpdateAsync(UpdateMedicalRecordCommand cmd, CancellationToken ct);
}
