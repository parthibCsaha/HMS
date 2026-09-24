namespace HMS.Application.Features.LabOrders.DTOs;

public record LabResultDto(
    Guid Id,
    string Result,
    string? Unit,
    string? ReferenceRange,
    string? Interpretation,
    DateTime ResultedAt,
    string? Notes,
    bool IsAbnormal
);
