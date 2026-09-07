using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class DepartmentRepository(AppDbContext context) : Repository<Department>(context), IDepartmentRepository
{
    public async Task<bool> CodeExistsAsync(string code, CancellationToken ct = default)
    {
        return await Context.Departments.AnyAsync(d => d.Code == code && !d.IsDeleted, ct);
    }

    public async Task<bool> NameExistsAsync(string name, Guid? excludeId = null, CancellationToken ct = default)
    {
        var q = Context.Departments.Where(d => d.Name == name && !d.IsDeleted);
        if (excludeId.HasValue) q = q.Where(d => d.Id != excludeId.Value);
        return await q.AnyAsync(ct);
    }

    public async Task<(IEnumerable<Department> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default)
    {
        var q = Context.Departments
            .Include(d => d.HeadDoctor).ThenInclude(doc => doc!.User)
            .Where(d => !d.IsDeleted)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(d => d.Name.ToLower().Contains(search) || d.Code.ToLower().Contains(search));
        }

        q = query.SortBy?.ToLower() switch
        {
            "name" => query.IsDescending ? q.OrderByDescending(d => d.Name) : q.OrderBy(d => d.Name),
            "code" => query.IsDescending ? q.OrderByDescending(d => d.Code) : q.OrderBy(d => d.Code),
            _ => q.OrderByDescending(d => d.CreatedAt)
        };

        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }
}
