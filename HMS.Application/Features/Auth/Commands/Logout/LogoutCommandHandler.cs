using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler(
    IUserRepository userRepository,
    HMS.Application.Common.Interfaces.IUnitOfWork unitOfWork,
    ILogger<LogoutCommandHandler> logger
) : IRequestHandler<LogoutCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(LogoutCommand command, CancellationToken ct)
    {
        await userRepository.UpdateRefreshTokenAsync(command.UserId, null, null, ct);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("User {UserId} logged out", command.UserId);
        return ApiResponse.Success("Logged out successfully.");
    }
}
