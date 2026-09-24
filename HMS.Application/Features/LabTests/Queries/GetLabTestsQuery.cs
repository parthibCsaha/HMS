using HMS.Application.Common.Models;
using HMS.Application.Features.LabTests.DTOs;
using MediatR;

namespace HMS.Application.Features.LabTests.Queries;

public record GetLabTestsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    string? SortBy = null
) : IRequest<ApiResponse<PaginatedResponse<LabTestListItemDto>>>;
