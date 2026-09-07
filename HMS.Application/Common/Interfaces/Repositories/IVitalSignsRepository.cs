using HMS.Domain.Entities;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IVitalSignsRepository : IRepository<VitalSigns>
{
    Task<IEnumerable<VitalSigns>> GetByPatientAsync(Guid patientId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
    Task<VitalSigns?> GetLatestByPatientAsync(Guid patientId, CancellationToken ct = default);
}
