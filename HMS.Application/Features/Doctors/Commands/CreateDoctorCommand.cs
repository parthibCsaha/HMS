using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

[RequirePermission(Permissions.DoctorsCreate)]
public record CreateDoctorCommand(
    Guid UserId,
    Guid DepartmentId,
    string Specialization,
    string Qualification,
    string LicenseNumber,
    int ExperienceYears,
    decimal ConsultationFee,
    string? Biography,
    List<DayOfWeek> AvailableDays,
    TimeSpan? ConsultationStartTime,
    TimeSpan? ConsultationEndTime,
    int SlotDurationMinutes = 15
) : IRequest<ApiResponse<Guid>>;
