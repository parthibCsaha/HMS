using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public record UpdateDepartmentCommand(
    Guid Id,
    string Name,
    string? Description,
    Guid? HeadDoctorId,
    string? Location,
    string? Phone,
    string? Email,
    bool IsActive
) : IRequest<ApiResponse>;
