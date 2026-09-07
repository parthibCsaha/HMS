using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.DTOs;
using HMS.Application.Features.Medications.Services;
using MediatR;

namespace HMS.Application.Features.Medications.Queries;

public class GetLowStockMedicationsQueryHandler(IMedicationService svc) : IRequestHandler<GetLowStockMedicationsQuery, ApiResponse<IEnumerable<MedicationListItemDto>>>
{
    public async Task<ApiResponse<IEnumerable<MedicationListItemDto>>> Handle(GetLowStockMedicationsQuery r, CancellationToken ct)
        => await svc.GetLowStockAsync(ct);
}
