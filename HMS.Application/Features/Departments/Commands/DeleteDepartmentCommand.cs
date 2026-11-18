using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

[RequirePermission(Permissions.DepartmentsDelete)]
public record DeleteDepartmentCommand(Guid Id) : IRequest<ApiResponse>;
