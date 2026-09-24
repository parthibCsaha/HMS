namespace HMS.Application.Features.MedicalRecords.DTOs;

public record MedicalRecordListItemDto(
    Guid Id,
    string RecordCode,
    string DoctorName,
    DateTime VisitDate,
    string Diagnosis,
    string? ChiefComplaint,
    bool IsConfidential
);
