using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public class DeleteWardCommandHandler(
    IWardRepository repo,
    IUnitOfWork uow
) : IRequestHandler<DeleteWardCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteWardCommand cmd, CancellationToken ct)
    {
        var ward = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Ward", cmd.Id);

        repo.SoftDelete(ward);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Ward deleted.");
    }
}
