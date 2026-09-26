using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IPasswordService passwordService,
    IUnitOfWork unitOfWork,
    ILogger<ChangePasswordCommandHandler> logger,
    ICurrentUserService currentUser
) : IRequestHandler<ChangePasswordCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ChangePasswordCommand command, CancellationToken ct)
    {
        if (currentUser.UserId is null)
            throw new UnauthorizedException("User not authenticated.");

        var userId = currentUser.UserId.Value;
        var user =
            await userRepository.GetByIdAsync(userId, ct)
            ?? throw new NotFoundException("User", userId);

        if (!passwordService.VerifyPassword(command.CurrentPassword, user.PasswordHash))
        {
            throw new BadRequestException("Current password is incorrect.");
        }

        if (!passwordService.IsStrongPassword(command.NewPassword))
        {
            throw new BadRequestException(
                "New password must be at least 8 characters with uppercase, lowercase, digit, and special character."
            );
        }

        user.PasswordHash = passwordService.HashPassword(command.NewPassword);

        // Invalidate refresh token so all existing sessions require re-login.
        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;

        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("User {UserId} changed password", userId);
        return ApiResponse.Success("Password changed successfully.");
    }
}
