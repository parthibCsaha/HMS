using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.Services;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public class CreatePrescriptionCommandHandler(IPrescriptionService svc) : IRequestHandler<CreatePrescriptionCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreatePrescriptionCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
