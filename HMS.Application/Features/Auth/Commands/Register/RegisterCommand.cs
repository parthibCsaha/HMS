using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Password,
        UserRole Role
    ) : IRequest<ApiResponse<AuthResponseDto>>;
}
