using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public class UpdateWardCommandHandler(
    IWardRepository repo,
    IUnitOfWork uow
) : IRequestHandler<UpdateWardCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateWardCommand cmd, CancellationToken ct)
    {
        var ward =
            await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Ward", cmd.Id);

        ward.Name = cmd.Name;
        ward.Description = cmd.Description;
        ward.Location = cmd.Location;
        ward.ChargePerDay = cmd.ChargePerDay;
        ward.IsActive = cmd.IsActive;

        repo.Update(ward);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Ward updated.");
    }
}
