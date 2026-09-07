using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.Services;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CheckInAppointmentCommandHandler(IAppointmentService svc) : IRequestHandler<CheckInAppointmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CheckInAppointmentCommand r, CancellationToken ct)
        => await svc.CheckInAsync(r.Id, ct);
}
