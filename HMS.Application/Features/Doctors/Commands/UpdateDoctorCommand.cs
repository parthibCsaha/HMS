using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public record UpdateDoctorCommand(
    Guid Id,
    Guid DepartmentId,
    string Specialization,
    string Qualification,
    string LicenseNumber,
    int ExperienceYears,
    decimal ConsultationFee,
    bool IsAvailable,
    string? Biography,
    string? AvailableDays,
    TimeSpan? ConsultationStartTime,
    TimeSpan? ConsultationEndTime,
    int SlotDurationMinutes = 15
) : IRequest<ApiResponse>;
