using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.Services;
using MediatR;

namespace HMS.Application.Features.LabOrders.Commands;

public class AddLabResultCommandHandler(ILabOrderService svc) : IRequestHandler<AddLabResultCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(AddLabResultCommand r, CancellationToken ct)
        => await svc.AddResultAsync(r, ct);
}
