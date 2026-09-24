namespace HMS.Application.Features.Staff.DTOs;

public record StaffDetailDto(
    Guid Id,
    Guid UserId,
    string StaffCode,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string StaffType,
    Guid? DepartmentId,
    string? DepartmentName,
    Guid? WardId,
    string? WardName,
    string Qualification,
    DateTime JoiningDate,
    string? Shift,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
