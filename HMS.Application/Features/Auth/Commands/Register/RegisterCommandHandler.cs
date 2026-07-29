using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Auth.Commands.DTOs;
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

        }
    }
}
