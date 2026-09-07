using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.DTOs;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Queries;

public record GetMedicalRecordByIdQuery(Guid Id) : IRequest<ApiResponse<MedicalRecordDetailDto>>;
