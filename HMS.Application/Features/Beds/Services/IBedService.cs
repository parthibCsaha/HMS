using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.Commands;
using HMS.Application.Features.Beds.DTOs;

namespace HMS.Application.Features.Beds.Services;

public interface IBedService
{
    Task<ApiResponse<IEnumerable<BedListItemDto>>> GetByWardAsync(Guid wardId, CancellationToken ct);
    Task<ApiResponse<BedDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateBedCommand cmd, CancellationToken ct);
    Task<ApiResponse> UpdateStatusAsync(Guid id, string status, CancellationToken ct);
    Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct);
}
