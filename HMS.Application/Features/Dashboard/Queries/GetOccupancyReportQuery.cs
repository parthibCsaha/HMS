using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Dashboard.Queries;

public record GetOccupancyReportQuery : IRequest<ApiResponse<IEnumerable<OccupancyReportDto>>>;
