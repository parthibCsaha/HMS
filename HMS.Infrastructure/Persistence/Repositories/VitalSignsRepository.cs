using HMS.Application.Common.Interfaces.Repositories;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class VitalSignsRepository(AppDbContext context)
    : Repository<VitalSigns>(context),
        IVitalSignsRepository
{
    public async Task<IEnumerable<VitalSigns>> GetByPatientAsync(
        Guid patientId,
        DateTime? from = null,
        DateTime? to = null,
        CancellationToken ct = default
    )
    {
        var q = Context.VitalSigns.Where(v => v.PatientId == patientId && !v.IsDeleted);
        if (from.HasValue)
            q = q.Where(v => v.RecordedAt >= from.Value);
        if (to.HasValue)
            q = q.Where(v => v.RecordedAt <= to.Value);
        return await q.OrderByDescending(v => v.RecordedAt).AsNoTracking().ToListAsync(ct);
    }

    public async Task<VitalSigns?> GetLatestByPatientAsync(
        Guid patientId,
        CancellationToken ct = default
    )
    {
        return await Context
            .VitalSigns.Where(v => v.PatientId == patientId && !v.IsDeleted)
            .OrderByDescending(v => v.RecordedAt)
            .FirstOrDefaultAsync(ct);
    }
}
