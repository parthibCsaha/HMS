using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public class ChangeBedStatusCommandHandler(
    IBedRepository repo,
    IUnitOfWork uow
) : IRequestHandler<ChangeBedStatusCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ChangeBedStatusCommand cmd, CancellationToken ct)
    {
        var bed = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Bed", cmd.Id);
        bed.Status = Enum.Parse<BedStatus>(cmd.Status);

        repo.Update(bed);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Bed status updated.");
    }
}
