using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.Logout;

/// <summary>
/// UserId is resolved internally from ICurrentUserService — no need to pass it from the controller.
/// </summary>
public record LogoutCommand : IRequest<ApiResponse>;
