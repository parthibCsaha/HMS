using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Application.Features.Auth.Services;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request, cancellationToken);
    }
}
