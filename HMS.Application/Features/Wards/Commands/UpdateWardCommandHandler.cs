using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.Services;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public class UpdateWardCommandHandler(IWardService svc) : IRequestHandler<UpdateWardCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateWardCommand r, CancellationToken ct)
        => await svc.UpdateAsync(r, ct);
}
