using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public class UpdateMedicationCommandHandler(
    IMedicationRepository repo,
    IUnitOfWork uow
) : IRequestHandler<UpdateMedicationCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateMedicationCommand cmd, CancellationToken ct)
    {
        var med =
            await repo.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException("Medication", cmd.Id);

        med.Name = cmd.Name;
        med.GenericName = cmd.GenericName;
        med.Category = cmd.Category;
        med.UnitPrice = cmd.UnitPrice;
        med.StockQuantity = cmd.StockQuantity;
        med.ReorderLevel = cmd.ReorderLevel;
        med.IsActive = cmd.IsActive;

        repo.Update(med);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Medication updated.");
    }
}
