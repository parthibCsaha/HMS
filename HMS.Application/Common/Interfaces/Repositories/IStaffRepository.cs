using HMS.Domain.Entities;
using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IStaffRepository : IRepository<Staff>
{
    Task<Staff?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<(IEnumerable<Staff> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default);
}
