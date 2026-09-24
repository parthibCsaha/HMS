using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CompleteAppointmentCommandHandler(
    IAppointmentRepository repo,
    IUnitOfWork uow
) : IRequestHandler<CompleteAppointmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CompleteAppointmentCommand cmd, CancellationToken ct)
    {
        var appointment =
            await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Appointment", cmd.Id);

        appointment.Status = AppointmentStatus.Completed;
        appointment.CompletedAt = DateTime.UtcNow;

        repo.Update(appointment);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Appointment completed.");
    }
}
