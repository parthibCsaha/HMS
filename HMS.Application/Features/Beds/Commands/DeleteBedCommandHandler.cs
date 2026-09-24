using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public class DeleteBedCommandHandler(
    IBedRepository repo,
    IUnitOfWork uow
) : IRequestHandler<DeleteBedCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteBedCommand cmd, CancellationToken ct)
    {
        var bed = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Bed", cmd.Id);

        repo.SoftDelete(bed);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Bed deleted.");
    }
}
