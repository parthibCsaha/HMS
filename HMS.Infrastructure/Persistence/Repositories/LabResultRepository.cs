using HMS.Application.Common.Interfaces.Repositories;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class LabResultRepository(AppDbContext context) : Repository<LabResult>(context), ILabResultRepository
{
    public async Task<IEnumerable<LabResult>> GetByLabOrderAsync(Guid labOrderId, CancellationToken ct = default)
    {
        return await Context.LabResults
            .Include(r => r.LabTest)
            .Where(r => r.LabOrderId == labOrderId && !r.IsDeleted)
            .OrderBy(r => r.ResultedAt).AsNoTracking().ToListAsync(ct);
    }

    public async Task<IEnumerable<LabResult>> GetByPatientAsync(Guid patientId, CancellationToken ct = default)
    {
        return await Context.LabResults
            .Include(r => r.LabTest)
            .Include(r => r.LabOrder)
            .Where(r => r.PatientId == patientId && !r.IsDeleted)
            .OrderByDescending(r => r.ResultedAt).AsNoTracking().ToListAsync(ct);
    }
}
