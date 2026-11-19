using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

[RequirePermission(Permissions.StaffDelete)]
public record DeleteStaffCommand(Guid Id) : IRequest<ApiResponse>;
