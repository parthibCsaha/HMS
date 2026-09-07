using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.Commands;
using HMS.Application.Features.Departments.DTOs;

namespace HMS.Application.Features.Departments.Services;

public interface IDepartmentService
{
    Task<ApiResponse<PaginatedResponse<DepartmentListItemDto>>> GetDepartmentsAsync(PaginationQuery q, CancellationToken ct);
    Task<ApiResponse<DepartmentDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateDepartmentCommand cmd, CancellationToken ct);
    Task<ApiResponse> UpdateAsync(UpdateDepartmentCommand cmd, CancellationToken ct);
    Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct);
}
