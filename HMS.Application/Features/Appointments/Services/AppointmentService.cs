using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.Commands;
using HMS.Application.Features.Appointments.DTOs;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Appointments.Services;

public class AppointmentService(IAppointmentRepository repo, IPatientRepository patientRepo, IDoctorRepository doctorRepo,
    ICodeGeneratorService codeGen, IUnitOfWork uow, ILogger<AppointmentService> log) : IAppointmentService
{
    public async Task<ApiResponse<PaginatedResponse<AppointmentListItemDto>>> GetAppointmentsAsync(PaginationQuery q, Guid? doctorId, Guid? patientId, AppointmentStatus? status, DateTime? date, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(q, doctorId, patientId, status, date, ct);
        var dtos = items.Select(a => new AppointmentListItemDto(a.Id, a.AppointmentCode,
            $"{a.Patient.User.FirstName} {a.Patient.User.LastName}", $"{a.Doctor.User.FirstName} {a.Doctor.User.LastName}",
            a.Department.Name, a.AppointmentDate, a.AppointmentTime, a.DurationMinutes,
            a.Type.ToString(), a.Status.ToString(), a.IsPaid, a.ConsultationFee ?? 0m));
        return ApiResponse<PaginatedResponse<AppointmentListItemDto>>.Success(PaginatedResponse<AppointmentListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<AppointmentDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var a = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Appointment", id);
        if (a.Patient?.User is null) { var p = await patientRepo.GetByIdAsync(a.PatientId, ct); a.Patient = p!; }
        if (a.Doctor?.User is null) { var d = await doctorRepo.GetByIdAsync(a.DoctorId, ct); a.Doctor = d!; }
        return ApiResponse<AppointmentDetailDto>.Success(new AppointmentDetailDto(a.Id, a.AppointmentCode,
            a.PatientId, $"{a.Patient.User.FirstName} {a.Patient.User.LastName}",
            a.DoctorId, $"{a.Doctor.User.FirstName} {a.Doctor.User.LastName}",
            a.DepartmentId, a.Department?.Name ?? "", a.AppointmentDate, a.AppointmentTime, a.DurationMinutes,
            a.Type.ToString(), a.Status.ToString(), a.ChiefComplaint, a.Notes, a.CancellationReason,
            a.IsPaid, a.ConsultationFee ?? 0m, a.ReferredByDoctorId, a.CheckedInAt, a.CompletedAt, a.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateAppointmentCommand cmd, CancellationToken ct)
    {
        if (await repo.HasConflictAsync(cmd.DoctorId, cmd.AppointmentDate, cmd.AppointmentTime, cmd.DurationMinutes, null, ct))
            throw new ConflictException("Doctor has a scheduling conflict at that time.");
        var code = await codeGen.GenerateCodeAsync("APT", ct);
        var appt = new Appointment
        {
            AppointmentCode = code, PatientId = cmd.PatientId, DoctorId = cmd.DoctorId,
            DepartmentId = cmd.DepartmentId, AppointmentDate = cmd.AppointmentDate,
            AppointmentTime = cmd.AppointmentTime, DurationMinutes = cmd.DurationMinutes,
            Type = Enum.Parse<AppointmentType>(cmd.Type), Status = AppointmentStatus.Scheduled,
            ChiefComplaint = cmd.ChiefComplaint, Notes = cmd.Notes,
            ConsultationFee = cmd.ConsultationFee, ReferredByDoctorId = cmd.ReferredByDoctorId
        };
        await repo.AddAsync(appt, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(appt.Id, "Appointment created.");
    }

    public async Task<ApiResponse> CancelAsync(Guid id, string reason, CancellationToken ct)
    {
        var a = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Appointment", id);
        a.Status = AppointmentStatus.Cancelled; a.CancellationReason = reason;
        repo.Update(a); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Appointment cancelled.");
    }

    public async Task<ApiResponse> CheckInAsync(Guid id, CancellationToken ct)
    {
        var a = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Appointment", id);
        a.Status = AppointmentStatus.CheckedIn; a.CheckedInAt = DateTime.UtcNow;
        repo.Update(a); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Patient checked in.");
    }

    public async Task<ApiResponse> CompleteAsync(Guid id, CancellationToken ct)
    {
        var a = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Appointment", id);
        a.Status = AppointmentStatus.Completed; a.CompletedAt = DateTime.UtcNow;
        repo.Update(a); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Appointment completed.");
    }
}
