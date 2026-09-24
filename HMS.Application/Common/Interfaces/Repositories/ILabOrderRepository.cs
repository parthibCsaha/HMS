using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface ILabOrderRepository : IRepository<LabOrder>
{
    Task<(IEnumerable<LabOrder> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? patientId = null,
        LabTestStatus? status = null,
        CancellationToken ct = default
    );
    Task<LabOrder?> GetWithItemsAsync(Guid id, CancellationToken ct = default);
}
