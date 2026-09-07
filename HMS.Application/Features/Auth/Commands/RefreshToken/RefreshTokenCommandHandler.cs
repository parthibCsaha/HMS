using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Application.Features.Auth.Services;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await authService.RefreshTokenAsync(request, cancellationToken);
    }
}
