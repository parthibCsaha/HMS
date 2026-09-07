using HMS.Domain.Entities;
using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<bool> DoctorCodeExistsAsync(string code, CancellationToken ct = default);
    Task<(IEnumerable<Doctor> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default);
    Task<IEnumerable<Doctor>> GetByDepartmentAsync(Guid departmentId, CancellationToken ct = default);
}
