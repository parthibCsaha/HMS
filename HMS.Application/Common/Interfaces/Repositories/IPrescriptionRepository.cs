using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IPrescriptionRepository : IRepository<Prescription>
{
    Task<(IEnumerable<Prescription> Items, int TotalCount)> GetPagedByPatientAsync(
        Guid patientId,
        PaginationQuery query,
        CancellationToken ct = default
    );
    Task<Prescription?> GetWithItemsAsync(Guid id, CancellationToken ct = default);
}
