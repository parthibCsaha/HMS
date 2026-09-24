using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public record UpdateStaffCommand(
    Guid Id,
    string StaffType,
    Guid? DepartmentId,
    Guid? WardId,
    string Qualification,
    string? Shift,
    bool IsActive
) : IRequest<ApiResponse>;
