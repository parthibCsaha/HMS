using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.Services;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public class DeleteWardCommandHandler(IWardService svc) : IRequestHandler<DeleteWardCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteWardCommand r, CancellationToken ct)
        => await svc.DeleteAsync(r.Id, ct);
}
