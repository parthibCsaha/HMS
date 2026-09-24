using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.SystemSettings.DTOs;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Queries;

public class GetSettingsByCategoryQueryHandler(ISystemSettingRepository repo)
    : IRequestHandler<GetSettingsByCategoryQuery, ApiResponse<IEnumerable<SystemSettingDto>>>
{
    public async Task<ApiResponse<IEnumerable<SystemSettingDto>>> Handle(
        GetSettingsByCategoryQuery r,
        CancellationToken ct
    )
    {
        var items = await repo.GetByCategoryAsync(r.Category, ct);
        return ApiResponse<IEnumerable<SystemSettingDto>>.Success(
            items.Select(s => new SystemSettingDto(s.Id, s.Key, s.Value, s.Category, s.Description))
        );
    }
}
