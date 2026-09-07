using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.DTOs;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public record GetPatientByIdQuery(Guid Id) : IRequest<ApiResponse<PatientDetailDto>>;
