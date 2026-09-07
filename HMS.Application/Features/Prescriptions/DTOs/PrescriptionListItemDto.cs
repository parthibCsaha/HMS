namespace HMS.Application.Features.Prescriptions.DTOs;

public record PrescriptionListItemDto(Guid Id, string PrescriptionCode, string DoctorName,
    DateTime IssuedDate, bool IsDispensed, int ItemCount);
