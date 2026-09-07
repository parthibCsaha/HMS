using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.Services;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public class CreateMedicationCommandHandler(IMedicationService svc) : IRequestHandler<CreateMedicationCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateMedicationCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
