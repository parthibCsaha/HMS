using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public record GetDashboardStatsQuery : IRequest<ApiResponse<DashboardStatsDto>>;
