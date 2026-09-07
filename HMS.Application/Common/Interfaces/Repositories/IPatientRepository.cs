using HMS.Domain.Entities;
using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<bool> PatientCodeExistsAsync(string code, CancellationToken ct = default);
    Task<(IEnumerable<Patient> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default);
}
