using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.DTOs;
using HMS.Application.Features.Departments.Services;
using MediatR;

namespace HMS.Application.Features.Departments.Queries;

public class GetDepartmentByIdQueryHandler(IDepartmentService svc) : IRequestHandler<GetDepartmentByIdQuery, ApiResponse<DepartmentDetailDto>>
{
    public async Task<ApiResponse<DepartmentDetailDto>> Handle(GetDepartmentByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
