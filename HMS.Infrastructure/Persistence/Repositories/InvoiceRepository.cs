using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class InvoiceRepository(AppDbContext context) : Repository<Invoice>(context), IInvoiceRepository
{
    public async Task<(IEnumerable<Invoice> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, Guid? patientId = null, InvoiceStatus? status = null, CancellationToken ct = default)
    {
        var q = Context.Invoices
            .Include(i => i.Patient).ThenInclude(p => p.User)
            .Where(i => !i.IsDeleted).AsNoTracking();

        if (patientId.HasValue) q = q.Where(i => i.PatientId == patientId.Value);
        if (status.HasValue) q = q.Where(i => i.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(i => i.InvoiceNumber.ToLower().Contains(search) || i.Patient.User.FirstName.ToLower().Contains(search));
        }

        q = q.OrderByDescending(i => i.InvoiceDate);
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<Invoice?> GetWithItemsAsync(Guid id, CancellationToken ct = default)
    {
        return await Context.Invoices
            .Include(i => i.Patient).ThenInclude(p => p.User)
            .Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted, ct);
    }
}
