using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<(IEnumerable<Invoice> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? patientId = null,
        InvoiceStatus? status = null,
        CancellationToken ct = default
    );
    Task<Invoice?> GetWithItemsAsync(Guid id, CancellationToken ct = default);
}
