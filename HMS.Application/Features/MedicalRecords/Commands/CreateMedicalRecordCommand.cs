using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Commands;

[RequirePermission(Permissions.MedicalRecordsCreate)]
public record CreateMedicalRecordCommand(
    Guid PatientId,
    Guid DoctorId,
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
    bool IsConfidential = false
) : IRequest<ApiResponse<Guid>>;
