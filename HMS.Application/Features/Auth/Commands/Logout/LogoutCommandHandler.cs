using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUser,
    ILogger<LogoutCommandHandler> logger
) : IRequestHandler<LogoutCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(LogoutCommand command, CancellationToken ct)
    {
        if (currentUser.UserId is null)
            throw new UnauthorizedException("User not authenticated.");

        var userId = currentUser.UserId.Value;
        await userRepository.UpdateRefreshTokenAsync(userId, null, null, ct);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("User {UserId} logged out", userId);
        return ApiResponse.Success("Logged out successfully.");
    }
}
