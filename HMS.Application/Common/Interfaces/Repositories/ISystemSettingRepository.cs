using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface ISystemSettingRepository : IRepository<SystemSetting>
{
    Task<SystemSetting?> GetByKeyAsync(string key, CancellationToken ct = default);
    Task<IEnumerable<SystemSetting>> GetByCategoryAsync(string category, CancellationToken ct = default);
}
