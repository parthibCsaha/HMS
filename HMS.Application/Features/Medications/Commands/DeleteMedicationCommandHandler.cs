using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.Services;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public class DeleteMedicationCommandHandler(IMedicationService svc) : IRequestHandler<DeleteMedicationCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteMedicationCommand r, CancellationToken ct)
        => await svc.DeleteAsync(r.Id, ct);
}
