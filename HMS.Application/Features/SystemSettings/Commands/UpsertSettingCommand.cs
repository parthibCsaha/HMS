using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Commands;

public record UpsertSettingCommand(string Key, string Value, string? Category, string? Description)
    : IRequest<ApiResponse>;
