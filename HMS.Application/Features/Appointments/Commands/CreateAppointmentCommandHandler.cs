using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CreateAppointmentCommandHandler(
    IAppointmentRepository repo,
    ICodeGeneratorService codeGen,
    IUnitOfWork uow
) : IRequestHandler<CreateAppointmentCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateAppointmentCommand cmd, CancellationToken ct)
    {
        if (
            await repo.HasConflictAsync(
                cmd.DoctorId,
                cmd.AppointmentDate,
                cmd.AppointmentTime,
                cmd.DurationMinutes,
                null,
                ct
            )
        )
            throw new ConflictException("Doctor has a scheduling conflict at that time.");
        var code = await codeGen.GenerateCodeAsync("APT", ct);
        var appt = new Appointment
        {
            AppointmentCode = code,
            PatientId = cmd.PatientId,
            DoctorId = cmd.DoctorId,
            DepartmentId = cmd.DepartmentId,
            AppointmentDate = cmd.AppointmentDate,
            AppointmentTime = cmd.AppointmentTime,
            DurationMinutes = cmd.DurationMinutes,
            Type = Enum.Parse<AppointmentType>(cmd.Type),
            Status = AppointmentStatus.Scheduled,
            ChiefComplaint = cmd.ChiefComplaint,
            Notes = cmd.Notes,
            ConsultationFee = cmd.ConsultationFee,
            ReferredByDoctorId = cmd.ReferredByDoctorId,
        };
        await repo.AddAsync(appt, ct);
        await uow.SaveChangesAsync(ct);

        return ApiResponse<Guid>.Success(appt.Id, "Appointment created.");
    }
}

