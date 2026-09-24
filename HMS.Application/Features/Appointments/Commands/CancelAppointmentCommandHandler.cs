using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CancelAppointmentCommandHandler(
    IAppointmentRepository repo,
    IUnitOfWork uow
) : IRequestHandler<CancelAppointmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CancelAppointmentCommand cmd, CancellationToken ct)
    {
        var appointment =
            await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Appointment", cmd.Id);

        appointment.Status = AppointmentStatus.Cancelled;
        appointment.CancellationReason = cmd.Reason;

        repo.Update(appointment);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Appointment cancelled.");
    }
}
