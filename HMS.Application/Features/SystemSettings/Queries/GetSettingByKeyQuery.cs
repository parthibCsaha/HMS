using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.SystemSettings.DTOs;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Queries;

[RequirePermission(Permissions.SystemSettingsView)]
public record GetSettingByKeyQuery(string Key) : IRequest<ApiResponse<SystemSettingDto?>>;
