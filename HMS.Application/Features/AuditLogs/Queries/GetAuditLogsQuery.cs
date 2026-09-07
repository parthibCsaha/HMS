using HMS.Application.Common.Models;
using HMS.Application.Features.AuditLogs.DTOs;
using MediatR;

namespace HMS.Application.Features.AuditLogs.Queries;

public record GetAuditLogsQuery(int PageNumber = 1, int PageSize = 20, Guid? UserId = null,
    string? Action = null, string? EntityName = null, DateTime? From = null, DateTime? To = null)
    : IRequest<ApiResponse<PaginatedResponse<AuditLogDto>>>;
