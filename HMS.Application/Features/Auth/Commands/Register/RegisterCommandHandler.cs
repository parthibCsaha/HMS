using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
using HMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordService passwordService,
        ILogger<RegisterCommandHandler> logger)
        : IRequestHandler<RegisterCommand, ApiResponse<AuthResponseDto>>
    {
        public async Task<ApiResponse<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await userRepository.EmailExistsAsync(request.Email, cancellationToken))
                throw new ConflictException($"Email '{request.Email}' is already registered.");

            var refreshToken = jwtService.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(30);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.ToLowerInvariant(),
                Phone = request.Phone,
                PasswordHash = passwordService.HashPassword(request.Password),
                Role = request.Role,
                IsActive = true,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = refreshTokenExpiry
            };

            var userId = await userRepository.CreateAsync(user, cancellationToken);
            user.Id = userId;

            var accessToken = jwtService.GenerateAccessToken(user);
            var accessTokenExpiry = DateTime.UtcNow.AddMinutes(60);

            return ApiResponse<AuthResponseDto>.Success(
                new AuthResponseDto(
                    userId, 
                    user.FirstName, 
                    user.LastName, 
                    user.Email,
                    user.Role.ToString(), 
                    accessToken, 
                    refreshToken,
                    accessTokenExpiry, 
                    refreshTokenExpiry),
                "Registration successful.");
        }
    }
}
