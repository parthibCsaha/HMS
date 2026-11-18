using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

[RequirePermission(Permissions.DepartmentsCreate)]
public record CreateDepartmentCommand(
    string Name,
    string? Description,
    Guid? HeadDoctorId,
    string? Location,
    string? Phone,
    string? Email
) : IRequest<ApiResponse<Guid>>;
