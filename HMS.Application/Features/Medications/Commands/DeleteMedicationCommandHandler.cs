using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public class DeleteMedicationCommandHandler(
    IMedicationRepository repo,
    IUnitOfWork uow
) : IRequestHandler<DeleteMedicationCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteMedicationCommand cmd, CancellationToken ct)
    {
        var med = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Medication", cmd.Id);

        repo.SoftDelete(med);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Medication deleted.");
    }
}
