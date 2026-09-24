using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.DTOs;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public record GetDoctorsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    string? SortBy = null,
    bool IsDescending = false
) : IRequest<ApiResponse<PaginatedResponse<DoctorListItemDto>>>;
