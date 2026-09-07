using HMS.Domain.Entities;
using HMS.Domain.Enums;
using HMS.Application.Common.Models;

namespace HMS.Application.Common.Interfaces.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<(IEnumerable<Appointment> Items, int TotalCount)> GetPagedAsync(PaginationQuery query, Guid? doctorId = null, Guid? patientId = null, AppointmentStatus? status = null, DateTime? date = null, CancellationToken ct = default);
    Task<IEnumerable<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken ct = default);
    Task<bool> HasConflictAsync(Guid doctorId, DateTime date, TimeSpan startTime, int durationMinutes, Guid? excludeId = null, CancellationToken ct = default);
}
