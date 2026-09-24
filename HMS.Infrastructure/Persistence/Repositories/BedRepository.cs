using HMS.Application.Common.Interfaces.Repositories;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class BedRepository(AppDbContext context) : Repository<Bed>(context), IBedRepository
{
    public async Task<IEnumerable<Bed>> GetByWardAsync(Guid wardId, CancellationToken ct = default)
    {
        return await Context
            .Beds.Include(b => b.Ward)
            .Include(b => b.CurrentPatient)
                .ThenInclude(p => p!.User)
            .Where(b => b.WardId == wardId && !b.IsDeleted)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Bed>> GetAvailableByWardAsync(
        Guid wardId,
        CancellationToken ct = default
    )
    {
        return await Context
            .Beds.Where(b => b.WardId == wardId && b.Status == BedStatus.Available && !b.IsDeleted)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}
