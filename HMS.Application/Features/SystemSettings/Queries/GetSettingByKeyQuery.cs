using HMS.Application.Common.Models;
using HMS.Application.Features.SystemSettings.DTOs;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Queries;

public record GetSettingByKeyQuery(string Key) : IRequest<ApiResponse<SystemSettingDto?>>;
