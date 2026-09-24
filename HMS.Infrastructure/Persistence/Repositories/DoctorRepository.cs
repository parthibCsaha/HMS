using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class DoctorRepository(AppDbContext context) : Repository<Doctor>(context), IDoctorRepository
{
    public async Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await Context
            .Doctors.Include(d => d.User)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.UserId == userId && !d.IsDeleted, ct);
    }

    public async Task<bool> DoctorCodeExistsAsync(string code, CancellationToken ct = default)
    {
        return await Context.Doctors.AnyAsync(d => d.DoctorCode == code && !d.IsDeleted, ct);
    }

    public async Task<(IEnumerable<Doctor> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        CancellationToken ct = default
    )
    {
        var q = Context
            .Doctors.Include(d => d.User)
            .Include(d => d.Department)
            .Where(d => !d.IsDeleted)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(d =>
                d.DoctorCode.ToLower().Contains(search)
                || d.User.FirstName.ToLower().Contains(search)
                || d.User.LastName.ToLower().Contains(search)
                || d.Specialization.ToLower().Contains(search)
                || d.Department.Name.ToLower().Contains(search)
            );
        }

        q = query.SortBy?.ToLower() switch
        {
            "name" => query.IsDescending
                ? q.OrderByDescending(d => d.User.FirstName)
                : q.OrderBy(d => d.User.FirstName),
            "specialization" => query.IsDescending
                ? q.OrderByDescending(d => d.Specialization)
                : q.OrderBy(d => d.Specialization),
            "department" => query.IsDescending
                ? q.OrderByDescending(d => d.Department.Name)
                : q.OrderBy(d => d.Department.Name),
            "experience" => query.IsDescending
                ? q.OrderByDescending(d => d.ExperienceYears)
                : q.OrderBy(d => d.ExperienceYears),
            _ => q.OrderByDescending(d => d.CreatedAt),
        };

        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);

        return (items, totalCount);
    }

    public async Task<IEnumerable<Doctor>> GetByDepartmentAsync(
        Guid departmentId,
        CancellationToken ct = default
    )
    {
        return await Context
            .Doctors.Include(d => d.User)
            .Where(d => d.DepartmentId == departmentId && !d.IsDeleted)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}
