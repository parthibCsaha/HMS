using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.Services;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public class CreateAppointmentCommandHandler(IAppointmentService svc) : IRequestHandler<CreateAppointmentCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateAppointmentCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
