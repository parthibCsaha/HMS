using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

[RequirePermission(Permissions.WardsCreate)]
public record CreateWardCommand(
    string Name,
    string WardNumber,
    string WardType,
    Guid DepartmentId,
    int TotalBeds,
    string? Description,
    decimal ChargePerDay,
    string? Location
) : IRequest<ApiResponse<Guid>>;
