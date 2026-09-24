namespace HMS.Application.Features.LabOrders.DTOs;

public record LabOrderItemDto(
    Guid Id,
    string TestName,
    string TestCode,
    string Status,
    decimal Price,
    LabResultDto? Result
);
