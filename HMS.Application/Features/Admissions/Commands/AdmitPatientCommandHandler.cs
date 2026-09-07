using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.Services;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public class AdmitPatientCommandHandler(IAdmissionService svc) : IRequestHandler<AdmitPatientCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(AdmitPatientCommand r, CancellationToken ct)
        => await svc.AdmitAsync(r, ct);
}
