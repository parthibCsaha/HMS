using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.DTOs;
using HMS.Application.Features.Departments.Services;
using MediatR;

namespace HMS.Application.Features.Departments.Queries;

public class GetDepartmentsQueryHandler(IDepartmentService svc) : IRequestHandler<GetDepartmentsQuery, ApiResponse<PaginatedResponse<DepartmentListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<DepartmentListItemDto>>> Handle(GetDepartmentsQuery r, CancellationToken ct)
        => await svc.GetDepartmentsAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize, SearchTerm = r.SearchTerm }, ct);
}
