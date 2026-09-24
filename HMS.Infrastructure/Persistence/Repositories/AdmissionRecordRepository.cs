using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class AdmissionRecordRepository(AppDbContext context)
    : Repository<AdmissionRecord>(context),
        IAdmissionRecordRepository
{
    public async Task<(IEnumerable<AdmissionRecord> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? patientId = null,
        bool? isActive = null,
        CancellationToken ct = default
    )
    {
        var q = Context
            .AdmissionRecords.Include(a => a.Patient)
                .ThenInclude(p => p.User)
            .Include(a => a.AdmittingDoctor)
                .ThenInclude(d => d.User)
            .Include(a => a.Ward)
            .Include(a => a.Bed)
            .Where(a => !a.IsDeleted)
            .AsNoTracking();

        if (patientId.HasValue)
            q = q.Where(a => a.PatientId == patientId.Value);
        if (isActive.HasValue)
            q = q.Where(a => a.IsActive == isActive.Value);

        q = q.OrderByDescending(a => a.AdmissionDate);
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<AdmissionRecord?> GetActiveByPatientAsync(
        Guid patientId,
        CancellationToken ct = default
    )
    {
        return await Context
            .AdmissionRecords.Include(a => a.Ward)
            .Include(a => a.Bed)
            .FirstOrDefaultAsync(a => a.PatientId == patientId && a.IsActive && !a.IsDeleted, ct);
    }
}
