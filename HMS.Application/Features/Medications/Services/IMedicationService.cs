using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.Commands;
using HMS.Application.Features.Medications.DTOs;

namespace HMS.Application.Features.Medications.Services;

public interface IMedicationService
{
    Task<ApiResponse<PaginatedResponse<MedicationListItemDto>>> GetMedicationsAsync(PaginationQuery q, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateMedicationCommand cmd, CancellationToken ct);
    Task<ApiResponse> UpdateAsync(UpdateMedicationCommand cmd, CancellationToken ct);
    Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<IEnumerable<MedicationListItemDto>>> GetLowStockAsync(CancellationToken ct);
}
