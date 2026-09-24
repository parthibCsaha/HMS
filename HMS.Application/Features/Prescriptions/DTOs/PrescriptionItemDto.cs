namespace HMS.Application.Features.Prescriptions.DTOs;

public record PrescriptionItemDto(
    Guid Id,
    string MedicationName,
    string Dosage,
    string Frequency,
    string Route,
    int DurationDays,
    int Quantity,
    string? Instructions,
    bool IsDispensed
);
