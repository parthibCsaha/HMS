using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public record CreateStaffCommand(
    Guid UserId,
    string StaffType,
    Guid? DepartmentId,
    Guid? WardId,
    string Qualification,
    DateTime JoiningDate,
    string? Shift
) : IRequest<ApiResponse<Guid>>;
