using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IMedicationRepository : IRepository<Medication>
{
    Task<(IEnumerable<Medication> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        CancellationToken ct = default
    );
    Task<IEnumerable<Medication>> GetLowStockAsync(CancellationToken ct = default);
}
