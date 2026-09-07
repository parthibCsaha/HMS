using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.Services;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public class UpdateMedicationCommandHandler(IMedicationService svc) : IRequestHandler<UpdateMedicationCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateMedicationCommand r, CancellationToken ct)
        => await svc.UpdateAsync(r, ct);
}
