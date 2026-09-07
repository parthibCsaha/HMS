using HMS.Domain.Entities;
using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IMedicationRepository : IRepository<Medication>
{
    Task<(IEnumerable<Medication> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default);
    Task<IEnumerable<Medication>> GetLowStockAsync(CancellationToken ct = default);
}
