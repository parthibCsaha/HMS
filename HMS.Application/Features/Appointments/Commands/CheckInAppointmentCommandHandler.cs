using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CheckInAppointmentCommandHandler(
    IAppointmentRepository repo,
    IUnitOfWork uow
) : IRequestHandler<CheckInAppointmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CheckInAppointmentCommand cmd, CancellationToken ct)
    {
        var appointment =
            await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Appointment", cmd.Id);

        appointment.Status = AppointmentStatus.CheckedIn;
        appointment.CheckedInAt = DateTime.UtcNow;

        repo.Update(appointment);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Patient checked in.");
    }
}
