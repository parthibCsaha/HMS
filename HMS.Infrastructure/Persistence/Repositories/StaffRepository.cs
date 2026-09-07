using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class StaffRepository(AppDbContext context) : Repository<Staff>(context), IStaffRepository
{
    public async Task<Staff?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await Context.Staff
            .Include(s => s.User)
            .Include(s => s.Department)
            .Include(s => s.Ward)
            .FirstOrDefaultAsync(s => s.UserId == userId && !s.IsDeleted, ct);
    }

    public async Task<(IEnumerable<Staff> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default)
    {
        var q = Context.Staff
            .Include(s => s.User)
            .Include(s => s.Department)
            .Where(s => !s.IsDeleted)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(s =>
                s.StaffCode.ToLower().Contains(search) ||
                s.User.FirstName.ToLower().Contains(search) ||
                s.User.LastName.ToLower().Contains(search));
        }

        q = query.SortBy?.ToLower() switch
        {
            "name" => query.IsDescending ? q.OrderByDescending(s => s.User.FirstName) : q.OrderBy(s => s.User.FirstName),
            "type" => query.IsDescending ? q.OrderByDescending(s => s.StaffType) : q.OrderBy(s => s.StaffType),
            "department" => query.IsDescending ? q.OrderByDescending(s => s.Department!.Name) : q.OrderBy(s => s.Department!.Name),
            _ => q.OrderByDescending(s => s.CreatedAt)
        };

        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }
}
