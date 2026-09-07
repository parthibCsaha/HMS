using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.DTOs;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public record GetPrescriptionByIdQuery(Guid Id) : IRequest<ApiResponse<PrescriptionDetailDto>>;
