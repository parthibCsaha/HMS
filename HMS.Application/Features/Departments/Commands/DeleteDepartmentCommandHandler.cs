using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.Services;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public class DeleteDepartmentCommandHandler(IDepartmentService svc) : IRequestHandler<DeleteDepartmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteDepartmentCommand r, CancellationToken ct)
        => await svc.DeleteAsync(r.Id, ct);
}
