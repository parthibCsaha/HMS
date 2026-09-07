using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Application.Features.Auth.Services;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, ApiResponse<AuthResponseDto>>
{
    public async Task<ApiResponse<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await authService.RegisterAsync(request, cancellationToken);
    }
}
