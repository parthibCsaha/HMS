using HMS.Application.Common.Interfaces.Repositories;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class SystemSettingRepository(AppDbContext context)
    : Repository<SystemSetting>(context),
        ISystemSettingRepository
{
    public async Task<SystemSetting?> GetByKeyAsync(string key, CancellationToken ct = default)
    {
        return await Context.SystemSettings.FirstOrDefaultAsync(
            s => s.Key == key && !s.IsDeleted,
            ct
        );
    }

    public async Task<IEnumerable<SystemSetting>> GetByCategoryAsync(
        string category,
        CancellationToken ct = default
    )
    {
        return await Context
            .SystemSettings.Where(s => s.Category == category && !s.IsDeleted)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}
