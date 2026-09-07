using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<ApiResponse<AuthResponseDto>>;
