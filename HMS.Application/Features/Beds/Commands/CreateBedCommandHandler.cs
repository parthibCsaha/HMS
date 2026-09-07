using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.Services;
using MediatR;

namespace HMS.Application.Features.Beds.Commands;

public class CreateBedCommandHandler(IBedService svc) : IRequestHandler<CreateBedCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateBedCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
