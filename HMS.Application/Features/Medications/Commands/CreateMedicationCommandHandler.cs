using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public class CreateMedicationCommandHandler(
    IMedicationRepository repo,
    IUnitOfWork uow
) : IRequestHandler<CreateMedicationCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateMedicationCommand cmd, CancellationToken ct)
    {
        var med = new Medication
        {
            Name = cmd.Name,
            GenericName = cmd.GenericName,
            Category = cmd.Category,
            Manufacturer = cmd.Manufacturer,
            DosageForm = cmd.DosageForm,
            Strength = cmd.Strength,
            UnitPrice = cmd.UnitPrice,
            StockQuantity = cmd.StockQuantity,
            ReorderLevel = cmd.ReorderLevel,
            ExpiryDate = cmd.ExpiryDate,
            IsActive = true,
        };
        await repo.AddAsync(med, ct);
        await uow.SaveChangesAsync(ct);

        return ApiResponse<Guid>.Success(med.Id, "Medication created.");
    }
}
