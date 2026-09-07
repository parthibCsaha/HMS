using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.DTOs;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public record GetPatientsQuery(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null,
    string? SortBy = null, bool IsDescending = false) : IRequest<ApiResponse<PaginatedResponse<PatientListItemDto>>>;
