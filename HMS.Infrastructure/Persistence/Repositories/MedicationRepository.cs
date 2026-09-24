using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class MedicationRepository(AppDbContext context)
    : Repository<Medication>(context),
        IMedicationRepository
{
    public async Task<(IEnumerable<Medication> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        CancellationToken ct = default
    )
    {
        var q = Context.Medications.Where(m => !m.IsDeleted).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(m =>
                m.Name.ToLower().Contains(search)
                || m.GenericName.ToLower().Contains(search)
                || m.Category.ToLower().Contains(search)
            );
        }
        q = query.SortBy?.ToLower() switch
        {
            "name" => query.IsDescending
                ? q.OrderByDescending(m => m.Name)
                : q.OrderBy(m => m.Name),
            "category" => query.IsDescending
                ? q.OrderByDescending(m => m.Category)
                : q.OrderBy(m => m.Category),
            "price" => query.IsDescending
                ? q.OrderByDescending(m => m.UnitPrice)
                : q.OrderBy(m => m.UnitPrice),
            "stock" => query.IsDescending
                ? q.OrderByDescending(m => m.StockQuantity)
                : q.OrderBy(m => m.StockQuantity),
            _ => q.OrderByDescending(m => m.CreatedAt),
        };
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<IEnumerable<Medication>> GetLowStockAsync(CancellationToken ct = default)
    {
        return await Context
            .Medications.Where(m => m.StockQuantity <= m.ReorderLevel && m.IsActive && !m.IsDeleted)
            .OrderBy(m => m.StockQuantity)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}
