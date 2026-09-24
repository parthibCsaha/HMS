using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface ILabResultRepository : IRepository<LabResult>
{
    Task<IEnumerable<LabResult>> GetByLabOrderAsync(
        Guid labOrderId,
        CancellationToken ct = default
    );
    Task<IEnumerable<LabResult>> GetByPatientAsync(Guid patientId, CancellationToken ct = default);
}
