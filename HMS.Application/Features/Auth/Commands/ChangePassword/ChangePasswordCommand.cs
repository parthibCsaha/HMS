using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Auth.Commands.ChangePassword;

public record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword,
    string ConfirmNewPassword
) : IRequest<ApiResponse>;
