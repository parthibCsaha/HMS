using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface INursingNoteRepository : IRepository<NursingNote>
{
    Task<(IEnumerable<NursingNote> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? patientId = null,
        Guid? admissionId = null,
        CancellationToken ct = default
    );
}
