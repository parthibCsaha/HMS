using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.SystemSettings.Commands;

public class UpsertSettingCommandHandler(ISystemSettingRepository repo, IUnitOfWork uow)
    : IRequestHandler<UpsertSettingCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpsertSettingCommand cmd, CancellationToken ct)
    {
        var existing = await repo.GetByKeyAsync(cmd.Key, ct);
        if (existing != null)
        {
            existing.Value = cmd.Value;
            existing.Description = cmd.Description;
            repo.Update(existing);
        }
        else
        {
            await repo.AddAsync(
                new SystemSetting
                {
                    Key = cmd.Key,
                    Value = cmd.Value,
                    Category = cmd.Category ?? "General",
                    Description = cmd.Description,
                },
                ct
            );
        }
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Setting saved.");
    }
}
