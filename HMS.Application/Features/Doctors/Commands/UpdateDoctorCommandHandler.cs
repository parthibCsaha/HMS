using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.Services;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class UpdateDoctorCommandHandler(IDoctorService svc) : IRequestHandler<UpdateDoctorCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateDoctorCommand r, CancellationToken ct)
        => await svc.UpdateDoctorAsync(r, ct);
}
