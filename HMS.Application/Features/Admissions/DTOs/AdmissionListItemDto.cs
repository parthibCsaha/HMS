namespace HMS.Application.Features.Admissions.DTOs;

public record AdmissionListItemDto(
    Guid Id,
    string AdmissionCode,
    string PatientName,
    string DoctorName,
    string WardName,
    string BedNumber,
    DateTime AdmissionDate,
    bool IsActive
);
