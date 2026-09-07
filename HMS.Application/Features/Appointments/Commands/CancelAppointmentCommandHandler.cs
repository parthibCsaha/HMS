using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.Services;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CancelAppointmentCommandHandler(IAppointmentService svc) : IRequestHandler<CancelAppointmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CancelAppointmentCommand r, CancellationToken ct)
        => await svc.CancelAsync(r.Id, r.Reason, ct);
}
