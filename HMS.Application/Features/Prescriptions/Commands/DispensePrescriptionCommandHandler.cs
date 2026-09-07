using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.Services;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public class DispensePrescriptionCommandHandler(IPrescriptionService svc) : IRequestHandler<DispensePrescriptionCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DispensePrescriptionCommand r, CancellationToken ct)
        => await svc.DispenseAsync(r.Id, ct);
}
