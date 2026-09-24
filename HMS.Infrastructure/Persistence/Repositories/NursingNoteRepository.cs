using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class NursingNoteRepository(AppDbContext context)
    : Repository<NursingNote>(context),
        INursingNoteRepository
{
    public async Task<(IEnumerable<NursingNote> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? patientId = null,
        Guid? admissionId = null,
        CancellationToken ct = default
    )
    {
        var q = Context
            .NursingNotes.Include(n => n.Patient)
                .ThenInclude(p => p.User)
            .Where(n => !n.IsDeleted)
            .AsNoTracking();
        if (patientId.HasValue)
            q = q.Where(n => n.PatientId == patientId.Value);
        if (admissionId.HasValue)
            q = q.Where(n => n.AdmissionId == admissionId.Value);
        q = q.OrderByDescending(n => n.NoteDateTime);
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }
}
