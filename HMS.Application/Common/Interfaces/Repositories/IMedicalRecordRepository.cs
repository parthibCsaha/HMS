using HMS.Domain.Entities;
using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<(IEnumerable<MedicalRecord> Items, int TotalCount)> GetPagedByPatientAsync(Guid patientId, PaginationQuery query, CancellationToken ct = default);
}
