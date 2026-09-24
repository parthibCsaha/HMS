using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.DTOs;
using MediatR;

namespace HMS.Application.Features.Medications.Queries;

public class GetMedicationsQueryHandler(IMedicationRepository repo)
    : IRequestHandler<GetMedicationsQuery, ApiResponse<PaginatedResponse<MedicationListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<MedicationListItemDto>>> Handle(
        GetMedicationsQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, SearchTerm = request.SearchTerm },
            ct
        );

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

        var response = PaginatedResponse<MedicationListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<MedicationListItemDto>>.Success(response);
    }
}
