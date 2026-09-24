using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public record CreatePrescriptionItemInput(
    Guid MedicationId,
    string Dosage,
    string Frequency,
    string Route,
    int DurationDays,
    int Quantity,
    string? Instructions
);

public record CreatePrescriptionCommand(
    Guid MedicalRecordId,
    Guid PatientId,
    Guid DoctorId,
    DateTime? ExpiryDate,
    string? Instructions,
    string? Notes,
    List<CreatePrescriptionItemInput> Items
) : IRequest<ApiResponse<Guid>>;
