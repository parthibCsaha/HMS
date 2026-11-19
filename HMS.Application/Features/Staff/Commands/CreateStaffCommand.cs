using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

[RequirePermission(Permissions.StaffCreate)]
public record CreateStaffCommand(
    Guid UserId,
    string StaffType,
    Guid? DepartmentId,
    Guid? WardId,
    string Qualification,
    DateTime JoiningDate,
    string? Shift
) : IRequest<ApiResponse<Guid>>;
