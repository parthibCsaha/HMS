using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.Services;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public class ChangeBedStatusCommandHandler(IBedService svc) : IRequestHandler<ChangeBedStatusCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(ChangeBedStatusCommand r, CancellationToken ct)
        => await svc.UpdateStatusAsync(r.Id, r.Status, ct);
}
