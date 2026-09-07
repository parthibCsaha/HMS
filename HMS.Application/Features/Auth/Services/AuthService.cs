using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.ChangePassword;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Application.Features.Auth.Commands.Login;
using HMS.Application.Features.Auth.Commands.Logout;
using HMS.Application.Features.Auth.Commands.RefreshToken;
using HMS.Application.Features.Auth.Commands.Register;
using HMS.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Auth.Services;

public class AuthService(
    IUserRepository userRepository,
    IJwtService jwtService,
    IPasswordService passwordService,
    IUnitOfWork unitOfWork,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterCommand command, CancellationToken ct)
    {
        if (await userRepository.EmailExistsAsync(command.Email, ct))
            throw new ConflictException($"Email '{command.Email}' is already registered.");

        if (!passwordService.IsStrongPassword(command.Password))
            throw new BadRequestException("Password must be at least 8 characters with uppercase, lowercase, digit, and special character.");

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Phone = command.Phone,
            PasswordHash = passwordService.HashPassword(command.Password),
            Role = command.Role,
            IsActive = true,
            RefreshToken = jwtService.GenerateRefreshToken(),
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(30)
        };

        await userRepository.AddAsync(user, ct);
        await unitOfWork.SaveChangesAsync(ct);

        var accessToken = jwtService.GenerateAccessToken(user);

        logger.LogInformation("User {Email} registered successfully with role {Role}", user.Email, user.Role);

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
            "Registration successful."
        );
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, ct)
            ?? throw new NotFoundException("User", command.Email);

        if (!user.IsActive)
            throw new UnauthorizedException("Your account has been deactivated. Please contact admin.");

        if (!passwordService.VerifyPassword(command.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");

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
                user.RefreshTokenExpiry.Value),
            "Login successful."
        );
    }

    public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByRefreshTokenAsync(command.RefreshToken, ct)
            ?? throw new UnauthorizedException("Invalid refresh token.");

        if (user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token has expired. Please log in again.");

        user.RefreshToken = jwtService.GenerateRefreshToken();
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(ct);

        var accessToken = jwtService.GenerateAccessToken(user);

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
                user.RefreshTokenExpiry.Value),
            "Token refreshed successfully."
        );
    }

    public async Task<ApiResponse> LogoutAsync(Guid userId, CancellationToken ct)
    {
        await userRepository.UpdateRefreshTokenAsync(userId, null, null, ct);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("User {UserId} logged out", userId);
        return ApiResponse.Success("Logged out successfully.");
    }

    public async Task<ApiResponse> ChangePasswordAsync(Guid userId, ChangePasswordCommand command, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(userId, ct)
            ?? throw new NotFoundException("User", userId);

        if (!passwordService.VerifyPassword(command.CurrentPassword, user.PasswordHash))
            throw new BadRequestException("Current password is incorrect.");

        if (!passwordService.IsStrongPassword(command.NewPassword))
            throw new BadRequestException("New password must be at least 8 characters with uppercase, lowercase, digit, and special character.");

        user.PasswordHash = passwordService.HashPassword(command.NewPassword);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("User {UserId} changed password", userId);
        return ApiResponse.Success("Password changed successfully.");
    }
}
