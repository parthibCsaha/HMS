using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.DTOs;
using HMS.Application.Features.Appointments.Services;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public class GetAppointmentByIdQueryHandler(IAppointmentService svc) : IRequestHandler<GetAppointmentByIdQuery, ApiResponse<AppointmentDetailDto>>
{
    public async Task<ApiResponse<AppointmentDetailDto>> Handle(GetAppointmentByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
