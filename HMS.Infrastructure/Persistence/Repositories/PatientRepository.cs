using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class PatientRepository(AppDbContext context) : Repository<Patient>(context), IPatientRepository
{
    public async Task<Patient?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await Context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted, ct);
    }

    public async Task<bool> PatientCodeExistsAsync(string code, CancellationToken ct = default)
    {
        return await Context.Patients.AnyAsync(p => p.PatientCode == code && !p.IsDeleted, ct);
    }

    public async Task<(IEnumerable<Patient> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, CancellationToken ct = default)
    {
        var q = Context.Patients
            .Include(p => p.User)
            .Where(p => !p.IsDeleted)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(p =>
                p.PatientCode.ToLower().Contains(search) ||
                p.User.FirstName.ToLower().Contains(search) ||
                p.User.LastName.ToLower().Contains(search) ||
                p.User.Email.ToLower().Contains(search) ||
                p.User.Phone.Contains(search) ||
                p.City.ToLower().Contains(search));
        }

        q = query.SortBy?.ToLower() switch
        {
            "name" => query.IsDescending ? q.OrderByDescending(p => p.User.FirstName) : q.OrderBy(p => p.User.FirstName),
            "code" => query.IsDescending ? q.OrderByDescending(p => p.PatientCode) : q.OrderBy(p => p.PatientCode),
            "dateofbirth" => query.IsDescending ? q.OrderByDescending(p => p.DateOfBirth) : q.OrderBy(p => p.DateOfBirth),
            "city" => query.IsDescending ? q.OrderByDescending(p => p.City) : q.OrderBy(p => p.City),
            _ => q.OrderByDescending(p => p.CreatedAt)
        };

        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);

        return (items, totalCount);
    }
}
