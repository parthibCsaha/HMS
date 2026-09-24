using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public record GetRevenueReportQuery(DateTime From, DateTime To)
    : IRequest<ApiResponse<IEnumerable<RevenueSummaryDto>>>;
