using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public record CreateAppointmentCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid DepartmentId,
    DateTime AppointmentDate,
    TimeSpan AppointmentTime,
    int DurationMinutes,
    string Type,
    string? ChiefComplaint,
    string? Notes,
    decimal ConsultationFee,
    Guid? ReferredByDoctorId
) : IRequest<ApiResponse<Guid>>;
