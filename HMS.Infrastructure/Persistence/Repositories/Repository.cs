using HMS.Application.Common.Interfaces.Repositories;
using HMS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class Repository<T>(AppDbContext context) : IRepository<T>
    where T : BaseEntity
{
    protected readonly AppDbContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        // Use FirstOrDefaultAsync instead of FindAsync to honor global query filters (soft delete).
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await DbSet.ToListAsync(ct);
    }

    public virtual async Task<Guid> AddAsync(T entity, CancellationToken ct = default)
    {
        await DbSet.AddAsync(entity, ct);
        return entity.Id;
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public virtual void SoftDelete(T entity, Guid? deletedBy = null)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        entity.DeletedBy = deletedBy;
        DbSet.Update(entity);
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await DbSet.AnyAsync(e => e.Id == id, ct);
    }

    public virtual async Task<int> CountAsync(CancellationToken ct = default)
    {
        return await DbSet.CountAsync(ct);
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return DbSet.AsNoTracking();
    }
}
