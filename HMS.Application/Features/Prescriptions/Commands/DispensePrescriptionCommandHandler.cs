using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public class DispensePrescriptionCommandHandler(
    IPrescriptionRepository repo,
    IMedicationRepository medRepo,
    IUnitOfWork uow
) : IRequestHandler<DispensePrescriptionCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DispensePrescriptionCommand cmd, CancellationToken ct)
    {
        var prescription =
            await repo.GetWithItemsAsync(cmd.Id, ct) ?? throw new NotFoundException("Prescription", cmd.Id);

        prescription.IsDispensed = true;
        prescription.DispensedAt = DateTime.UtcNow;

        foreach (var item in prescription.Items)
        {
            item.IsDispensed = true;
            var med = await medRepo.GetByIdAsync(item.MedicationId, ct);

            if (med != null)
            {
                med.StockQuantity -= item.Quantity;
                medRepo.Update(med);
            }
        }

        repo.Update(prescription);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Prescription dispensed.");
    }
}
