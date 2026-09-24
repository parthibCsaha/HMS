namespace HMS.Application.Features.Doctors.DTOs;

public record DoctorDetailDto(
    Guid Id,
    Guid UserId,
    string DoctorCode,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    Guid DepartmentId,
    string DepartmentName,
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
    int SlotDurationMinutes,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
