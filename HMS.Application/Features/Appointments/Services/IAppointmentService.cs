using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.Commands;
using HMS.Application.Features.Appointments.DTOs;

namespace HMS.Application.Features.Appointments.Services;

public interface IAppointmentService
{
    Task<ApiResponse<PaginatedResponse<AppointmentListItemDto>>> GetAppointmentsAsync(PaginationQuery q, Guid? doctorId, Guid? patientId, HMS.Domain.Enums.AppointmentStatus? status, DateTime? date, CancellationToken ct);
    Task<ApiResponse<AppointmentDetailDto>> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreateAsync(CreateAppointmentCommand cmd, CancellationToken ct);
    Task<ApiResponse> CancelAsync(Guid id, string reason, CancellationToken ct);
    Task<ApiResponse> CheckInAsync(Guid id, CancellationToken ct);
    Task<ApiResponse> CompleteAsync(Guid id, CancellationToken ct);
}
