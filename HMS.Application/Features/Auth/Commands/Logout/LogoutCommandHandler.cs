using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Services;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler(IAuthService authService) : IRequestHandler<LogoutCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return await authService.LogoutAsync(request.UserId, cancellationToken);
    }
}
