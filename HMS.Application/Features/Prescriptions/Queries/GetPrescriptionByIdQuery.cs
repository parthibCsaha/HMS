using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.Prescriptions.DTOs;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

[RequirePermission(Permissions.PrescriptionsView)]
public record GetPrescriptionByIdQuery(Guid Id) : IRequest<ApiResponse<PrescriptionDetailDto>>;
