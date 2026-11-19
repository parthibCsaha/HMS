using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

[RequirePermission(Permissions.WardsUpdate)]
public record UpdateWardCommand(
    Guid Id,
    string Name,
    string? Description,
    string? Location,
    decimal ChargePerDay,
    bool IsActive
) : IRequest<ApiResponse>;
