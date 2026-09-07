using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.Services;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class CreateDoctorCommandHandler(IDoctorService svc) : IRequestHandler<CreateDoctorCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateDoctorCommand r, CancellationToken ct)
        => await svc.CreateDoctorAsync(r, ct);
}
