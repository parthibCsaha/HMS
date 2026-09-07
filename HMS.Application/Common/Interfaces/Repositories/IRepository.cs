using HMS.Domain.Common;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task<Guid> AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void SoftDelete(T entity, Guid? deletedBy = null);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
    IQueryable<T> GetQueryable();
}
