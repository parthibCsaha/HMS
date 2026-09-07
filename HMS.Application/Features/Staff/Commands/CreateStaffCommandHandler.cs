using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.Services;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public class CreateStaffCommandHandler(IStaffService svc) : IRequestHandler<CreateStaffCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateStaffCommand r, CancellationToken ct)
        => await svc.CreateStaffAsync(r, ct);
}
