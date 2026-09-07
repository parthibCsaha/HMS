namespace HMS.Application.Features.LabOrders.DTOs;

public record LabOrderListItemDto(Guid Id, string OrderCode, string PatientName, string DoctorName,
    string Status, DateTime OrderDate, int ItemCount);
