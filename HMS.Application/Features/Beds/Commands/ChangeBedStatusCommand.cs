using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

[RequirePermission(Permissions.BedsUpdateStatus)]
public record ChangeBedStatusCommand(Guid Id, string Status) : IRequest<ApiResponse>;
