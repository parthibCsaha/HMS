using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.Services;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public class DeleteStaffCommandHandler(IStaffService svc) : IRequestHandler<DeleteStaffCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteStaffCommand r, CancellationToken ct)
        => await svc.DeleteStaffAsync(r.Id, ct);
}
