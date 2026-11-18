using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

[RequirePermission(Permissions.DepartmentsUpdate)]
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
