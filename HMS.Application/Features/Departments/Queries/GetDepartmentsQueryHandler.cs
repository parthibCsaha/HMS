using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.DTOs;
using MediatR;

namespace HMS.Application.Features.Departments.Queries;

public class GetDepartmentsQueryHandler(IDepartmentRepository repo)
    : IRequestHandler<GetDepartmentsQuery, ApiResponse<PaginatedResponse<DepartmentListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<DepartmentListItemDto>>> Handle(
        GetDepartmentsQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, SearchTerm = request.SearchTerm },
            ct
        );

        var dtos = items.Select(dept => new DepartmentListItemDto(
            dept.Id,
            dept.Code,
            dept.Name,
            dept.HeadDoctor != null
                ? $"{dept.HeadDoctor.User.FirstName} {dept.HeadDoctor.User.LastName}"
                : null,
            dept.Location,
            dept.IsActive
        ));

        var response = PaginatedResponse<DepartmentListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<DepartmentListItemDto>>.Success(response);
    }
}
