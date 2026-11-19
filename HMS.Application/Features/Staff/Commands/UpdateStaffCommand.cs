using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

[RequirePermission(Permissions.StaffUpdate)]
public record UpdateStaffCommand(
    Guid Id,
    string StaffType,
    Guid? DepartmentId,
    Guid? WardId,
    string Qualification,
    string? Shift,
    bool IsActive
) : IRequest<ApiResponse>;
