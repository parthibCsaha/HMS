using HMS.Application.Common.Models;
using HMS.Application.Features.SystemSettings.DTOs;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Queries;

public record GetSettingsByCategoryQuery(string Category)
    : IRequest<ApiResponse<IEnumerable<SystemSettingDto>>>;
