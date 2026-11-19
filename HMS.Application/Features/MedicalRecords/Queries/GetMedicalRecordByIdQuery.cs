using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.MedicalRecords.DTOs;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Queries;

[RequirePermission(Permissions.MedicalRecordsView)]
public record GetMedicalRecordByIdQuery(Guid Id) : IRequest<ApiResponse<MedicalRecordDetailDto>>;
