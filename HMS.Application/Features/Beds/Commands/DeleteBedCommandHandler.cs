using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.Services;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public class DeleteBedCommandHandler(IBedService svc) : IRequestHandler<DeleteBedCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteBedCommand r, CancellationToken ct)
        => await svc.DeleteAsync(r.Id, ct);
}
