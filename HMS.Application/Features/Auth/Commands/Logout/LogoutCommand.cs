using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.Logout;

public record LogoutCommand(Guid UserId) : IRequest<ApiResponse>;
