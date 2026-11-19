using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Commands;

[RequirePermission(Permissions.SystemSettingsUpsert)]
public record UpsertSettingCommand(string Key, string Value, string? Category, string? Description)
    : IRequest<ApiResponse>;
