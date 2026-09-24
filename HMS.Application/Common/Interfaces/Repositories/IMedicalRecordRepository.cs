using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<(IEnumerable<MedicalRecord> Items, int TotalCount)> GetPagedByPatientAsync(
        Guid patientId,
        PaginationQuery query,
        CancellationToken ct = default
    );
}
