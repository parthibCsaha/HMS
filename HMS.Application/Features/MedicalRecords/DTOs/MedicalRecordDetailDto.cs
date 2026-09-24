namespace HMS.Application.Features.MedicalRecords.DTOs;

public record MedicalRecordDetailDto(
    Guid Id,
    string RecordCode,
    Guid PatientId,
    Guid DoctorId,
    string DoctorName,
    Guid? AppointmentId,
    Guid? AdmissionId,
    DateTime VisitDate,
    string? ChiefComplaint,
    string? PresentIllnessHistory,
    string? PastMedicalHistory,
    string? FamilyHistory,
    string? SocialHistory,
    string? ReviewOfSystems,
    string? PhysicalExamination,
    string Diagnosis,
    string? DifferentialDiagnosis,
    string? Treatment,
    string? Procedures,
    string? Notes,
    string? FollowUpInstructions,
    DateTime? FollowUpDate,
    bool IsConfidential,
    DateTime CreatedAt
);
