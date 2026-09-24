using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.DTOs;
using MediatR;

namespace HMS.Application.Features.Departments.Queries;

public record GetDepartmentsQuery(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null)
    : IRequest<ApiResponse<PaginatedResponse<DepartmentListItemDto>>>;
