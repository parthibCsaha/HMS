using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.Commands;
using HMS.Application.Features.Prescriptions.DTOs;

namespace HMS.Application.Features.Prescriptions.Services;

public interface IPrescriptionService
{
    Task<ApiResponse<PaginatedResponse<PrescriptionListItemDto>>> GetByPatientAsync(Guid patientId, PaginationQuery q, CancellationToken ct);
    Task<ApiResponse<PrescriptionDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreatePrescriptionCommand cmd, CancellationToken ct);
    Task<ApiResponse> DispenseAsync(Guid id, CancellationToken ct);
}
