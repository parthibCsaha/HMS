using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.Commands;
using HMS.Application.Features.Staff.DTOs;

namespace HMS.Application.Features.Staff.Services;

public interface IStaffService
{
    Task<ApiResponse<PaginatedResponse<StaffListItemDto>>> GetStaffAsync(PaginationQuery query, CancellationToken ct);
    Task<ApiResponse<StaffDetailDto>> GetStaffByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateStaffAsync(CreateStaffCommand cmd, CancellationToken ct);
    Task<ApiResponse> UpdateStaffAsync(UpdateStaffCommand cmd, CancellationToken ct);
    Task<ApiResponse> DeleteStaffAsync(Guid id, CancellationToken ct);
}
