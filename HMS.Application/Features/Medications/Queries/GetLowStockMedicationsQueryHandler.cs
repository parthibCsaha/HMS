using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.DTOs;
using MediatR;

namespace HMS.Application.Features.Medications.Queries;

public class GetLowStockMedicationsQueryHandler(IMedicationRepository repo)
    : IRequestHandler<GetLowStockMedicationsQuery, ApiResponse<IEnumerable<MedicationListItemDto>>>
{
    public async Task<ApiResponse<IEnumerable<MedicationListItemDto>>> Handle(
        GetLowStockMedicationsQuery request,
        CancellationToken ct
    )
    {
        var items = await repo.GetLowStockAsync(ct);

        var dtos = items.Select(medication => new MedicationListItemDto(
            medication.Id,
            medication.Name,
            medication.GenericName,
            medication.Category,
            medication.UnitPrice,
            medication.StockQuantity,
            medication.ReorderLevel,
            medication.IsActive
        ));

        return ApiResponse<IEnumerable<MedicationListItemDto>>.Success(dtos);
    }
}
