using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.Commands;
using HMS.Application.Features.Admissions.DTOs;

namespace HMS.Application.Features.Admissions.Services;

public interface IAdmissionService
{
    Task<ApiResponse<PaginatedResponse<AdmissionListItemDto>>> GetAdmissionsAsync(PaginationQuery q, Guid? patientId, bool? isActive, CancellationToken ct);
    Task<ApiResponse<AdmissionDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> AdmitAsync(AdmitPatientCommand cmd, CancellationToken ct);
    Task<ApiResponse> DischargeAsync(Guid id, DischargePatientCommand cmd, CancellationToken ct);
    Task<ApiResponse> TransferAsync(Guid id, TransferPatientCommand cmd, CancellationToken ct);
}
