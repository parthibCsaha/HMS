using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.DTOs;
using MediatR;

namespace HMS.Application.Features.Departments.Queries;

public record GetDepartmentByIdQuery(Guid Id) : IRequest<ApiResponse<DepartmentDetailDto>>;
