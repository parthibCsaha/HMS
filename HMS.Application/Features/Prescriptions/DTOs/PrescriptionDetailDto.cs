namespace HMS.Application.Features.Prescriptions.DTOs;

public record PrescriptionDetailDto(Guid Id, string PrescriptionCode, Guid PatientId, Guid DoctorId,
    string DoctorName, DateTime IssuedDate, DateTime? ExpiryDate, string? Instructions, string? Notes,
    bool IsDispensed, DateTime? DispensedAt, List<PrescriptionItemDto> Items, DateTime CreatedAt);
