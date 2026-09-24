using HMS.Application.Common.Models;
using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IAdmissionRecordRepository : IRepository<AdmissionRecord>
{
    Task<(IEnumerable<AdmissionRecord> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? patientId = null,
        bool? isActive = null,
        CancellationToken ct = default
    );
    Task<AdmissionRecord?> GetActiveByPatientAsync(Guid patientId, CancellationToken ct = default);
}
