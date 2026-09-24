using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IAuditLogRepository : IRepository<AuditLog>
{
    Task<(IEnumerable<AuditLog> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? userId = null,
        string? action = null,
        string? entityName = null,
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default
    );
}
