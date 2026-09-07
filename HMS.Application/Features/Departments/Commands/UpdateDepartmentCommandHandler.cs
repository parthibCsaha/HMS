using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.Services;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public class UpdateDepartmentCommandHandler(IDepartmentService svc) : IRequestHandler<UpdateDepartmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateDepartmentCommand r, CancellationToken ct)
        => await svc.UpdateAsync(r, ct);
}
