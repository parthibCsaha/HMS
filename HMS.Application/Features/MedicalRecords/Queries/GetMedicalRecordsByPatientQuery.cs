using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.DTOs;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Queries;

public record GetMedicalRecordsByPatientQuery(Guid PatientId, int PageNumber = 1, int PageSize = 10)
    : IRequest<ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>>;
