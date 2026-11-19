using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using HMS.Application.Features.VitalSigns.DTOs;
using MediatR;

namespace HMS.Application.Features.VitalSigns.Queries;

[RequirePermission(Permissions.VitalSignsView)]
public record GetLatestVitalsQuery(Guid PatientId) : IRequest<ApiResponse<VitalSignsDto?>>;
