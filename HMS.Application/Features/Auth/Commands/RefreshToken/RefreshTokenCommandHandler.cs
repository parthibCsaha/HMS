using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IUnitOfWork unitOfWork
) : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(RefreshTokenCommand command, CancellationToken ct)
    {
        var user =
            await userRepository.GetByRefreshTokenAsync(command.RefreshToken, ct)
            ?? throw new UnauthorizedException("Invalid refresh token.");

        if (user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            throw new UnauthorizedException("Refresh token has expired. Please log in again.");
        }

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
                user.RefreshTokenExpiry.Value
            ),
            "Token refreshed successfully."
        );
    }
}
