using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.DTOs;
using HMS.Application.Features.Wards.Services;
using MediatR;

namespace HMS.Application.Features.Wards.Queries;

public class GetWardsQueryHandler(IWardService svc) : IRequestHandler<GetWardsQuery, ApiResponse<PaginatedResponse<WardListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<WardListItemDto>>> Handle(GetWardsQuery r, CancellationToken ct)
        => await svc.GetWardsAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize, SearchTerm = r.SearchTerm }, ct);
}
