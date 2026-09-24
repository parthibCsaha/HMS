using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IPasswordService passwordService,
    IUnitOfWork unitOfWork,
    ILogger<RegisterCommandHandler> logger
) : IRequestHandler<RegisterCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(RegisterCommand command, CancellationToken ct)
    {
        if (await userRepository.EmailExistsAsync(command.Email, ct))
        {
            throw new ConflictException($"Email '{command.Email}' is already registered.");
        }

        if (!passwordService.IsStrongPassword(command.Password))
        {
            throw new BadRequestException(
                "Password must be at least 8 characters with uppercase, lowercase, digit, and special character."
            );
        }

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
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(30),
        };

        await userRepository.AddAsync(user, ct);
        await unitOfWork.SaveChangesAsync(ct);

        var accessToken = jwtService.GenerateAccessToken(user);

        logger.LogInformation(
            "User {Email} registered successfully with role {Role}",
            user.Email,
            user.Role
        );

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
}

