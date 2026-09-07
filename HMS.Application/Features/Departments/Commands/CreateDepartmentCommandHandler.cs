using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.Services;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public class CreateDepartmentCommandHandler(IDepartmentService svc) : IRequestHandler<CreateDepartmentCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateDepartmentCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
