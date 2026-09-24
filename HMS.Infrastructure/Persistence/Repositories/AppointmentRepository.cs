using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Persistence.Repositories;

public class AppointmentRepository(AppDbContext context)
    : Repository<Appointment>(context),
        IAppointmentRepository
{
    public async Task<(IEnumerable<Appointment> Items, int TotalCount)> GetPagedAsync(
        PaginationQuery query,
        Guid? doctorId = null,
        Guid? patientId = null,
        AppointmentStatus? status = null,
        DateTime? date = null,
        CancellationToken ct = default
    )
    {
        var q = Context
            .Appointments.Include(a => a.Patient)
                .ThenInclude(p => p.User)
            .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
            .Include(a => a.Department)
            .Where(a => !a.IsDeleted)
            .AsNoTracking();

        if (doctorId.HasValue)
            q = q.Where(a => a.DoctorId == doctorId.Value);
        if (patientId.HasValue)
            q = q.Where(a => a.PatientId == patientId.Value);
        if (status.HasValue)
            q = q.Where(a => a.Status == status.Value);
        if (date.HasValue)
            q = q.Where(a => a.AppointmentDate.Date == date.Value.Date);

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var search = query.SearchTerm.ToLower();
            q = q.Where(a =>
                a.AppointmentCode.ToLower().Contains(search)
                || a.Patient.User.FirstName.ToLower().Contains(search)
                || a.Patient.User.LastName.ToLower().Contains(search)
                || a.Doctor.User.FirstName.ToLower().Contains(search)
                || a.Doctor.User.LastName.ToLower().Contains(search)
            );
        }

        q = query.SortBy?.ToLower() switch
        {
            "date" => query.IsDescending
                ? q.OrderByDescending(a => a.AppointmentDate)
                : q.OrderBy(a => a.AppointmentDate),
            "patient" => query.IsDescending
                ? q.OrderByDescending(a => a.Patient.User.FirstName)
                : q.OrderBy(a => a.Patient.User.FirstName),
            "doctor" => query.IsDescending
                ? q.OrderByDescending(a => a.Doctor.User.FirstName)
                : q.OrderBy(a => a.Doctor.User.FirstName),
            "status" => query.IsDescending
                ? q.OrderByDescending(a => a.Status)
                : q.OrderBy(a => a.Status),
            _ => q.OrderByDescending(a => a.AppointmentDate)
                .ThenByDescending(a => a.AppointmentTime),
        };

        var totalCount = await q.CountAsync(ct);
        var items = await q.Skip(query.Offset).Take(query.PageSize).ToListAsync(ct);
        return (items, totalCount);
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorAndDateAsync(
        Guid doctorId,
        DateTime date,
        CancellationToken ct = default
    )
    {
        return await Context
            .Appointments.Where(a =>
                a.DoctorId == doctorId && a.AppointmentDate.Date == date.Date && !a.IsDeleted
            )
            .OrderBy(a => a.AppointmentTime)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<bool> HasConflictAsync(
        Guid doctorId,
        DateTime date,
        TimeSpan startTime,
        int durationMinutes,
        Guid? excludeId = null,
        CancellationToken ct = default
    )
    {
        var endTime = startTime.Add(TimeSpan.FromMinutes(durationMinutes));

        var q = Context.Appointments.Where(a =>
            a.DoctorId == doctorId
            && a.AppointmentDate.Date == date.Date
            && !a.IsDeleted
            && a.Status != AppointmentStatus.Cancelled
            && a.Status != AppointmentStatus.NoShow
        );

        if (excludeId.HasValue)
            q = q.Where(a => a.Id != excludeId.Value);

        return await q.AnyAsync(
            a =>
                a.AppointmentTime < endTime
                && a.AppointmentTime.Add(TimeSpan.FromMinutes(a.DurationMinutes)) > startTime,
            ct
        );
    }
}
