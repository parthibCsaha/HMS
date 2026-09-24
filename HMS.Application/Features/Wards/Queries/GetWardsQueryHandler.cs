using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.DTOs;
using MediatR;

namespace HMS.Application.Features.Wards.Queries;

public class GetWardsQueryHandler(IWardRepository repo)
    : IRequestHandler<GetWardsQuery, ApiResponse<PaginatedResponse<WardListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<WardListItemDto>>> Handle(
        GetWardsQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, SearchTerm = request.SearchTerm },
            ct
        );

        var dtos = items.Select(ward => new WardListItemDto(
            ward.Id,
            ward.Name,
            ward.WardNumber,
            ward.WardType.ToString(),
            ward.Department?.Name ?? "",
            ward.TotalBeds,
            ward.AvailableBeds,
            ward.ChargePerDay ?? 0m,
            ward.IsActive
        ));

        var response = PaginatedResponse<WardListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<WardListItemDto>>.Success(response);
    }
}
