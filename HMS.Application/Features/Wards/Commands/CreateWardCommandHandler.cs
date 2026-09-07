using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.Services;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public class CreateWardCommandHandler(IWardService svc) : IRequestHandler<CreateWardCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateWardCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
