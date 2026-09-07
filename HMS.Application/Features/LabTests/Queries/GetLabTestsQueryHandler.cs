using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.LabTests.DTOs;
using MediatR;

namespace HMS.Application.Features.LabTests.Queries;

public class GetLabTestsQueryHandler(ILabTestRepository repo) : IRequestHandler<GetLabTestsQuery, ApiResponse<PaginatedResponse<LabTestListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<LabTestListItemDto>>> Handle(GetLabTestsQuery r, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize, SearchTerm = r.SearchTerm, SortBy = r.SortBy }, ct);
        var dtos = items.Select(t => new LabTestListItemDto(t.Id, t.Code, t.Name, t.Category, t.Price, t.IsActive));
        return ApiResponse<PaginatedResponse<LabTestListItemDto>>.Success(PaginatedResponse<LabTestListItemDto>.Create(dtos, r.PageNumber, r.PageSize, total));
    }
}
