using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.DTOs;
using HMS.Application.Features.Staff.Services;
using MediatR;

namespace HMS.Application.Features.Staff.Queries;

public class GetStaffQueryHandler(IStaffService svc) : IRequestHandler<GetStaffQuery, ApiResponse<PaginatedResponse<StaffListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<StaffListItemDto>>> Handle(GetStaffQuery r, CancellationToken ct)
        => await svc.GetStaffAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize, SearchTerm = r.SearchTerm, SortBy = r.SortBy, IsDescending = r.IsDescending }, ct);
}
