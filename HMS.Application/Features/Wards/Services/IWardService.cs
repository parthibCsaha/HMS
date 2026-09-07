using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.Commands;
using HMS.Application.Features.Wards.DTOs;

namespace HMS.Application.Features.Wards.Services;

public interface IWardService
{
    Task<ApiResponse<PaginatedResponse<WardListItemDto>>> GetWardsAsync(PaginationQuery q, CancellationToken ct);
    Task<ApiResponse<WardDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateWardCommand cmd, CancellationToken ct);
    Task<ApiResponse> UpdateAsync(UpdateWardCommand cmd, CancellationToken ct);
    Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct);
}
