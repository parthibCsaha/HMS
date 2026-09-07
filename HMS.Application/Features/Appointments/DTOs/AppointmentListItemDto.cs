namespace HMS.Application.Features.Appointments.DTOs;

public record AppointmentListItemDto(Guid Id, string AppointmentCode, string PatientName, string DoctorName,
    string Department, DateTime AppointmentDate, TimeSpan AppointmentTime, int DurationMinutes,
    string Type, string Status, bool IsPaid, decimal ConsultationFee);
