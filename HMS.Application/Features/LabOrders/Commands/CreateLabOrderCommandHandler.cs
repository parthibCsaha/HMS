using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.Services;
using MediatR;

namespace HMS.Application.Features.LabOrders.Commands;

public class CreateLabOrderCommandHandler(ILabOrderService svc) : IRequestHandler<CreateLabOrderCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateLabOrderCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
