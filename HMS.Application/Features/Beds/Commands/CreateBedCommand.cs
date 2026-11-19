using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

[RequirePermission(Permissions.BedsCreate)]
public record CreateBedCommand(string BedNumber, Guid WardId, string? Notes)
    : IRequest<ApiResponse<Guid>>;
