using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class PrescriptionRepository(AppDbContext context)
    : Repository<Prescription>(context),
        IPrescriptionRepository
{
    public async Task<(IEnumerable<Prescription> Items, int TotalCount)> GetPagedByPatientAsync(
        Guid patientId,
        PaginationQuery query,
        CancellationToken ct = default
    )
    {
        var q = Context
            .Prescriptions.Include(p => p.Doctor)
                .ThenInclude(d => d.User)
            .Include(p => p.Items)
                .ThenInclude(i => i.Medication)
            .Where(p => p.PatientId == patientId && !p.IsDeleted)
            .AsNoTracking();

        q = q.OrderByDescending(p => p.IssuedDate);
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<Prescription?> GetWithItemsAsync(Guid id, CancellationToken ct = default)
    {
        return await Context
            .Prescriptions.Include(p => p.Patient)
                .ThenInclude(pt => pt.User)
            .Include(p => p.Doctor)
                .ThenInclude(d => d.User)
            .Include(p => p.Items)
                .ThenInclude(i => i.Medication)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);
    }
}
