using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IStaffRepository : IRepository<Staff>
{
    Task<Staff?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<(IEnumerable<Staff> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        CancellationToken ct = default
    );
}
