using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Services;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IAuthService authService, ICurrentUserService currentUser)
    : IRequestHandler<ChangePasswordCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId
            ?? throw new UnauthorizedAccessException("User not authenticated.");
        return await authService.ChangePasswordAsync(userId, request, cancellationToken);
    }
}
