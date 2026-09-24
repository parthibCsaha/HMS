using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.DTOs;
using MediatR;

namespace HMS.Application.Features.Staff.Queries;

public record GetStaffQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    string? SortBy = null,
    bool IsDescending = false
) : IRequest<ApiResponse<PaginatedResponse<StaffListItemDto>>>;
