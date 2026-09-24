using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IWardRepository : IRepository<Ward>
{
    Task<(IEnumerable<Ward> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        CancellationToken ct = default
    );
    Task<IEnumerable<Ward>> GetByDepartmentAsync(Guid departmentId, CancellationToken ct = default);
}
