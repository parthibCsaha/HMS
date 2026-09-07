using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.Commands;
using HMS.Application.Features.Doctors.DTOs;

namespace HMS.Application.Features.Doctors.Services;

public interface IDoctorService
{
    Task<ApiResponse<PaginatedResponse<DoctorListItemDto>>> GetDoctorsAsync(PaginationQuery query, CancellationToken ct);
    Task<ApiResponse<DoctorDetailDto>> GetDoctorByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateDoctorAsync(CreateDoctorCommand cmd, CancellationToken ct);
    Task<ApiResponse> UpdateDoctorAsync(UpdateDoctorCommand cmd, CancellationToken ct);
    Task<ApiResponse> DeleteDoctorAsync(Guid id, CancellationToken ct);
}
