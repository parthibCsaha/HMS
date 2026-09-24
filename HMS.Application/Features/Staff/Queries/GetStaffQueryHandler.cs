using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.DTOs;
using MediatR;

namespace HMS.Application.Features.Staff.Queries;

public class GetStaffQueryHandler(IStaffRepository staffRepo)
    : IRequestHandler<GetStaffQuery, ApiResponse<PaginatedResponse<StaffListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<StaffListItemDto>>> Handle(
        GetStaffQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await staffRepo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, SearchTerm = request.SearchTerm },
            ct
        );

        var dtos = items.Select(staff => new StaffListItemDto(
            staff.Id,
            staff.StaffCode,
            staff.User.FirstName,
            staff.User.LastName,
            staff.User.Email,
            staff.StaffType.ToString(),
            staff.Department?.Name,
            staff.IsActive
        ));

        var response = PaginatedResponse<StaffListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<StaffListItemDto>>.Success(response);
    }
}
