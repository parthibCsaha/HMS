namespace HMS.Application.Features.Staff.DTOs;

public record StaffListItemDto(
    Guid Id,
    string StaffCode,
    string FirstName,
    string LastName,
    string Email,
    string StaffType,
    string? DepartmentName,
    bool IsActive
);
