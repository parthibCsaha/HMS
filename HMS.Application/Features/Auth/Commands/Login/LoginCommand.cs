using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<ApiResponse<AuthResponseDto>>;
