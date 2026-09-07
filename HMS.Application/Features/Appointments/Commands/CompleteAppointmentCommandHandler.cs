using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.Services;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CompleteAppointmentCommandHandler(IAppointmentService svc) : IRequestHandler<CompleteAppointmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CompleteAppointmentCommand r, CancellationToken ct)
        => await svc.CompleteAsync(r.Id, ct);
}
