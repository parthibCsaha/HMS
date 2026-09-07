using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.DTOs;
using MediatR;

namespace HMS.Application.Features.Admissions.Queries;

public record GetAdmissionsQuery(int PageNumber = 1, int PageSize = 10, Guid? PatientId = null, bool? IsActive = null) : IRequest<ApiResponse<PaginatedResponse<AdmissionListItemDto>>>;
