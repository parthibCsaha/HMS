using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class LabTestRepository(AppDbContext context)
    : Repository<LabTest>(context),
        ILabTestRepository
{
    public async Task<(IEnumerable<LabTest> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        CancellationToken ct = default
    )
    {
        var q = Context.LabTests.Where(lt => !lt.IsDeleted).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(lt =>
                lt.Name.ToLower().Contains(search)
                || lt.Code.ToLower().Contains(search)
                || lt.Category.ToLower().Contains(search)
            );
        }
        q = query.SortBy?.ToLower() switch
        {
            "name" => query.IsDescending
                ? q.OrderByDescending(lt => lt.Name)
                : q.OrderBy(lt => lt.Name),
            "code" => query.IsDescending
                ? q.OrderByDescending(lt => lt.Code)
                : q.OrderBy(lt => lt.Code),
            "category" => query.IsDescending
                ? q.OrderByDescending(lt => lt.Category)
                : q.OrderBy(lt => lt.Category),
            "price" => query.IsDescending
                ? q.OrderByDescending(lt => lt.Price)
                : q.OrderBy(lt => lt.Price),
            _ => q.OrderByDescending(lt => lt.CreatedAt),
        };
        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<bool> CodeExistsAsync(string code, CancellationToken ct = default)
    {
        return await Context.LabTests.AnyAsync(lt => lt.Code == code && !lt.IsDeleted, ct);
    }
}
