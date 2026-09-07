using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.Services;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class DeleteDoctorCommandHandler(IDoctorService svc) : IRequestHandler<DeleteDoctorCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteDoctorCommand r, CancellationToken ct)
        => await svc.DeleteDoctorAsync(r.Id, ct);
}
