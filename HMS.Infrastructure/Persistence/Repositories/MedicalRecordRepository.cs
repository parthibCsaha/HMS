using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class MedicalRecordRepository(AppDbContext context) : Repository<MedicalRecord>(context), IMedicalRecordRepository
{
    public async Task<(IEnumerable<MedicalRecord> Items, int TotalCount)> GetPagedByPatientAsync(Guid patientId, PaginationQuery query, CancellationToken ct = default)
    {
        var q = Context.MedicalRecords
            .Include(mr => mr.Doctor).ThenInclude(d => d.User)
            .Include(mr => mr.Appointment)
            .Where(mr => mr.PatientId == patientId && !mr.IsDeleted).AsNoTracking();

        q = q.OrderByDescending(mr => mr.VisitDate);
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }
}
