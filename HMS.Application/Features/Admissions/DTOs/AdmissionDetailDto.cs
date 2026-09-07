namespace HMS.Application.Features.Admissions.DTOs;

public record AdmissionDetailDto(Guid Id, string AdmissionCode, Guid PatientId, string PatientName,
    Guid AdmittingDoctorId, string DoctorName, Guid WardId, string WardName, Guid BedId, string BedNumber,
    DateTime AdmissionDate, DateTime? DischargeDate, string ReasonForAdmission, string? Diagnosis,
    string? DischargeSummary, string? DischargeCondition, bool IsActive, DateTime CreatedAt);
