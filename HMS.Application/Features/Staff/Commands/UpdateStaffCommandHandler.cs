using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.Services;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public class UpdateStaffCommandHandler(IStaffService svc) : IRequestHandler<UpdateStaffCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateStaffCommand r, CancellationToken ct)
        => await svc.UpdateStaffAsync(r, ct);
}
