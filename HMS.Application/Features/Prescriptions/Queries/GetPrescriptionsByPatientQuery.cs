using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.DTOs;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public record GetPrescriptionsByPatientQuery(Guid PatientId, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponse<PaginatedResponse<PrescriptionListItemDto>>>;
