using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.ChangePassword;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Application.Features.Auth.Commands.Login;
using HMS.Application.Features.Auth.Commands.Logout;
using HMS.Application.Features.Auth.Commands.RefreshToken;
using HMS.Application.Features.Auth.Commands.Register;

namespace HMS.Application.Features.Auth.Services;

public interface IAuthService
{
    Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterCommand command, CancellationToken ct);
    Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginCommand command, CancellationToken ct);
    Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken ct);
    Task<ApiResponse> LogoutAsync(Guid userId, CancellationToken ct);
    Task<ApiResponse> ChangePasswordAsync(Guid userId, ChangePasswordCommand command, CancellationToken ct);
}
