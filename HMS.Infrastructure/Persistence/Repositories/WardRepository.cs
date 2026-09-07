using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class WardRepository(AppDbContext context) : Repository<Ward>(context), IWardRepository
{
    public async Task<(IEnumerable<Ward> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default)
    {
        var q = Context.Wards.Include(w => w.Department).Where(w => !w.IsDeleted).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(w => w.Name.ToLower().Contains(search) || w.WardNumber.ToLower().Contains(search));
        }
        q = query.SortBy?.ToLower() switch
        {
            "name" => query.IsDescending ? q.OrderByDescending(w => w.Name) : q.OrderBy(w => w.Name),
            _ => q.OrderByDescending(w => w.CreatedAt)
        };
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<IEnumerable<Ward>> GetByDepartmentAsync(Guid departmentId, CancellationToken ct = default)
    {
        return await Context.Wards.Include(w => w.Department)
            .Where(w => w.DepartmentId == departmentId && !w.IsDeleted).AsNoTracking().ToListAsync(ct);
    }
}
