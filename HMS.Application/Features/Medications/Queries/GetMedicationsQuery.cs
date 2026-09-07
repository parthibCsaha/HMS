using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.DTOs;
using MediatR;

namespace HMS.Application.Features.Medications.Queries;

public record GetMedicationsQuery(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null, string? SortBy = null)
    : IRequest<ApiResponse<PaginatedResponse<MedicationListItemDto>>>;
