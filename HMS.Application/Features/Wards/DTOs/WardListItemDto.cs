namespace HMS.Application.Features.Wards.DTOs;

public record WardListItemDto(
    Guid Id,
    string Name,
    string WardNumber,
    string WardType,
    string DepartmentName,
    int TotalBeds,
    int AvailableBeds,
    decimal ChargePerDay,
    bool IsActive
);
