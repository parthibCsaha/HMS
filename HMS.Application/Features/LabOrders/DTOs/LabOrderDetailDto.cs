namespace HMS.Application.Features.LabOrders.DTOs;

public record LabOrderDetailDto(
    Guid Id,
    string OrderCode,
    Guid PatientId,
    string PatientName,
    Guid OrderingDoctorId,
    string DoctorName,
    string Status,
    DateTime OrderDate,
    string? ClinicalNotes,
    string Priority,
    bool IsBilled,
    List<LabOrderItemDto> Items,
    DateTime CreatedAt
);
