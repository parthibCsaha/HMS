using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.DTOs;
using MediatR;

namespace HMS.Application.Features.Departments.Queries;

public class GetDepartmentByIdQueryHandler(IDepartmentRepository repo)
    : IRequestHandler<GetDepartmentByIdQuery, ApiResponse<DepartmentDetailDto>>
{
    public async Task<ApiResponse<DepartmentDetailDto>> Handle(
        GetDepartmentByIdQuery request,
        CancellationToken ct
    )
    {
        var dept = await repo.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("Department", request.Id);

        var dto = new DepartmentDetailDto(
            dept.Id,
            dept.Code,
            dept.Name,
            dept.Description,
            dept.HeadDoctorId,
            dept.HeadDoctor != null
                ? $"{dept.HeadDoctor.User.FirstName} {dept.HeadDoctor.User.LastName}"
                : null,
            dept.Location,
            dept.Phone,
            dept.Email,
            dept.IsActive,
            dept.CreatedAt
        );

        return ApiResponse<DepartmentDetailDto>.Success(dto);
    }
}
