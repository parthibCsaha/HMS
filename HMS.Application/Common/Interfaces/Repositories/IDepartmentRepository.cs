using HMS.Domain.Entities;
using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<bool> CodeExistsAsync(string code, CancellationToken ct = default);
    Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken ct = default);
    Task<(IEnumerable<Department> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default);
}
