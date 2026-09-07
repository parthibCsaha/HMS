using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.DTOs;
using HMS.Application.Features.Medications.Services;
using MediatR;

namespace HMS.Application.Features.Medications.Queries;

public class GetMedicationsQueryHandler(IMedicationService svc) : IRequestHandler<GetMedicationsQuery, ApiResponse<PaginatedResponse<MedicationListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<MedicationListItemDto>>> Handle(GetMedicationsQuery r, CancellationToken ct)
        => await svc.GetMedicationsAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize, SearchTerm = r.SearchTerm, SortBy = r.SortBy }, ct);
}
