namespace HMS.Application.Features.Appointments.DTOs;

public record AppointmentDetailDto(
    Guid Id,
    string AppointmentCode,
    Guid PatientId,
    string PatientName,
    Guid DoctorId,
    string DoctorName,
    Guid DepartmentId,
    string DepartmentName,
    DateTime AppointmentDate,
    TimeSpan AppointmentTime,
    int DurationMinutes,
    string Type,
    string Status,
    string? ChiefComplaint,
    string? Notes,
    string? CancellationReason,
    bool IsPaid,
    decimal ConsultationFee,
    Guid? ReferredByDoctorId,
    DateTime? CheckedInAt,
    DateTime? CompletedAt,
    DateTime CreatedAt
);
