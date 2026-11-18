using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

[RequirePermission(Permissions.DashboardView)]
public record GetRevenueReportQuery(DateTime From, DateTime To)
    : IRequest<ApiResponse<IEnumerable<RevenueSummaryDto>>>;
