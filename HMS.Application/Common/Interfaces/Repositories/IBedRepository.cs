using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IBedRepository : IRepository<Bed>
{
    Task<IEnumerable<Bed>> GetByWardAsync(Guid wardId, CancellationToken ct = default);
    Task<IEnumerable<Bed>> GetAvailableByWardAsync(Guid wardId, CancellationToken ct = default);
}
