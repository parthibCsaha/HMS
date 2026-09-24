using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface ILabTestRepository : IRepository<LabTest>
{
    Task<(IEnumerable<LabTest> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        CancellationToken ct = default
    );
    Task<bool> CodeExistsAsync(string code, CancellationToken ct = default);
}
