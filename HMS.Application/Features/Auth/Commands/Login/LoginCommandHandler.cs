using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IPasswordService passwordService,
    IUnitOfWork unitOfWork,
    ILogger<LoginCommandHandler> logger
) : IRequestHandler<LoginCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(LoginCommand command, CancellationToken ct)
    {
        var user =
            await userRepository.GetByEmailAsync(command.Email, ct)
            ?? throw new NotFoundException("User", command.Email);

        if (!user.IsActive)
        {
            throw new UnauthorizedException(
                "Your account has been deactivated. Please contact admin."
            );
        }

        if (!passwordService.VerifyPassword(command.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid email or password.");
        }

        user.LastLoginAt = DateTime.UtcNow;
        user.RefreshToken = jwtService.GenerateRefreshToken();
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(ct);

        var accessToken = jwtService.GenerateAccessToken(user);

        logger.LogInformation("User {Email} logged in successfully", user.Email);

        return ApiResponse<AuthResponseDto>.Success(
            new AuthResponseDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role.ToString(),
                accessToken,
                user.RefreshToken,
                DateTime.UtcNow.AddMinutes(60),
                user.RefreshTokenExpiry.Value
            ),
            "Login successful."
        );
    }
}

