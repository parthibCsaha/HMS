using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.DTOs;
using MediatR;

namespace HMS.Application.Features.Wards.Queries;

public record GetWardsQuery(int PageNumber = 1, int PageSize = 10, string? SearchTerm = null) : IRequest<ApiResponse<PaginatedResponse<WardListItemDto>>>;
