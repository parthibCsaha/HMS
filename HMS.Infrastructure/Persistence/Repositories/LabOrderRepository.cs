using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class LabOrderRepository(AppDbContext context) : Repository<LabOrder>(context), ILabOrderRepository
{
    public async Task<(IEnumerable<LabOrder> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, Guid? patientId = null, LabTestStatus? status = null, CancellationToken ct = default)
    {
        var q = Context.LabOrders
            .Include(lo => lo.Patient).ThenInclude(p => p.User)
            .Include(lo => lo.OrderingDoctor).ThenInclude(d => d.User)
            .Include(lo => lo.Items).ThenInclude(i => i.LabTest)
            .Where(lo => !lo.IsDeleted).AsNoTracking();

        if (patientId.HasValue) q = q.Where(lo => lo.PatientId == patientId.Value);
        if (status.HasValue) q = q.Where(lo => lo.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(lo => lo.OrderCode.ToLower().Contains(search) || lo.Patient.User.FirstName.ToLower().Contains(search));
        }

        q = q.OrderByDescending(lo => lo.OrderDate);
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<LabOrder?> GetWithItemsAsync(Guid id, CancellationToken ct = default)
    {
        return await Context.LabOrders
            .Include(lo => lo.Patient).ThenInclude(p => p.User)
            .Include(lo => lo.OrderingDoctor).ThenInclude(d => d.User)
            .Include(lo => lo.Items).ThenInclude(i => i.LabTest)
            .Include(lo => lo.Items).ThenInclude(i => i.Result)
            .FirstOrDefaultAsync(lo => lo.Id == id && !lo.IsDeleted, ct);
    }
}
