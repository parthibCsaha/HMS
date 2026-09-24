namespace HMS.Application.Features.Departments.DTOs;

public record DepartmentListItemDto(
    Guid Id,
    string Code,
    string Name,
    string? HeadDoctorName,
    string? Location,
    bool IsActive
);
