using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public class CreateBedCommandHandler(
    IBedRepository repo,
    IWardRepository wardRepo,
    IUnitOfWork uow
) : IRequestHandler<CreateBedCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateBedCommand cmd, CancellationToken ct)
    {
        if (!await wardRepo.ExistsAsync(cmd.WardId, ct))
        {
            throw new NotFoundException("Ward", cmd.WardId);
        }

        var bed = new Bed
        {
            BedNumber = cmd.BedNumber,
            WardId = cmd.WardId,
            Status = BedStatus.Available,
            Notes = cmd.Notes,
        };

        await repo.AddAsync(bed, ct);
        await uow.SaveChangesAsync(ct);

        return ApiResponse<Guid>.Success(bed.Id, "Bed created.");
    }
}
