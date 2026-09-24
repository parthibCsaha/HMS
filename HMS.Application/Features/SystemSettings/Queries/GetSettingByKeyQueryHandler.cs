using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.SystemSettings.DTOs;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Queries;

public class GetSettingByKeyQueryHandler(ISystemSettingRepository repo)
    : IRequestHandler<GetSettingByKeyQuery, ApiResponse<SystemSettingDto?>>
{
    public async Task<ApiResponse<SystemSettingDto?>> Handle(
        GetSettingByKeyQuery r,
        CancellationToken ct
    )
    {
        var s = await repo.GetByKeyAsync(r.Key, ct);
        return s is null
            ? ApiResponse<SystemSettingDto?>.Success(null)
            : ApiResponse<SystemSettingDto?>.Success(
                new SystemSettingDto(s.Id, s.Key, s.Value, s.Category, s.Description)
            );
    }
}
