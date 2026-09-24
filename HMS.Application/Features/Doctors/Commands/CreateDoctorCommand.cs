using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public record CreateDoctorCommand(
    Guid UserId,
    Guid DepartmentId,
    string Specialization,
    string Qualification,
    string LicenseNumber,
    int ExperienceYears,
    decimal ConsultationFee,
    string? Biography,
    string? AvailableDays,
    TimeSpan? ConsultationStartTime,
    TimeSpan? ConsultationEndTime,
    int SlotDurationMinutes = 15
) : IRequest<ApiResponse<Guid>>;
