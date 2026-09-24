namespace HMS.Application.Features.Wards.DTOs;

public record WardDetailDto(
    Guid Id,
    string Name,
    string WardNumber,
    string WardType,
    Guid DepartmentId,
    string DepartmentName,
    int TotalBeds,
    int AvailableBeds,
    string? Description,
    decimal ChargePerDay,
    string? Location,
    bool IsActive,
    DateTime CreatedAt
);
