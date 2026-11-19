using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

[RequirePermission(Permissions.BedsDelete)]
public record DeleteBedCommand(Guid Id) : IRequest<ApiResponse>;
