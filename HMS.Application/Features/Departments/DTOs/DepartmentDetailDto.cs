namespace HMS.Application.Features.Departments.DTOs;

public record DepartmentDetailDto(
    Guid Id,
    string Code,
    string Name,
    string? Description,
    Guid? HeadDoctorId,
    string? HeadDoctorName,
    string? Location,
    string? Phone,
    string? Email,
    bool IsActive,
    DateTime CreatedAt
);
