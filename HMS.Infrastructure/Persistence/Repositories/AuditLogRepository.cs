using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class AuditLogRepository(AppDbContext context)
    : Repository<AuditLog>(context),
        IAuditLogRepository
{
    public async Task<(IEnumerable<AuditLog> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? userId = null,
        string? action = null,
        string? entityName = null,
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default
    )
    {
        var q = Context.AuditLogs.Where(a => !a.IsDeleted).AsNoTracking();

        if (userId.HasValue)
            q = q.Where(a => a.UserId == userId.Value);
        if (!string.IsNullOrWhiteSpace(action))
            q = q.Where(a => a.Action == action);
        if (!string.IsNullOrWhiteSpace(entityName))
            q = q.Where(a => a.EntityName == entityName);
        if (from.HasValue)
            q = q.Where(a => a.LoggedAt >= from.Value);
        if (to.HasValue)
            q = q.Where(a => a.LoggedAt <= to.Value);

        q = q.OrderByDescending(a => a.LoggedAt);
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }
}
