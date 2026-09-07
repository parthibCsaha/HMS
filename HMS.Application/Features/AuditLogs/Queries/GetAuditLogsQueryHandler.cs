using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.AuditLogs.DTOs;
using MediatR;

namespace HMS.Application.Features.AuditLogs.Queries;

public class GetAuditLogsQueryHandler(IAuditLogRepository repo) : IRequestHandler<GetAuditLogsQuery, ApiResponse<PaginatedResponse<AuditLogDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<AuditLogDto>>> Handle(GetAuditLogsQuery r, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize },
            r.UserId, r.Action, r.EntityName, r.From, r.To, ct);
        var dtos = items.Select(a => new AuditLogDto(a.Id, a.UserId, a.Action, a.EntityName,
            a.EntityId, a.OldValues, a.NewValues, a.IpAddress, a.LoggedAt));
        return ApiResponse<PaginatedResponse<AuditLogDto>>.Success(
            PaginatedResponse<AuditLogDto>.Create(dtos, r.PageNumber, r.PageSize, total));
    }
}
