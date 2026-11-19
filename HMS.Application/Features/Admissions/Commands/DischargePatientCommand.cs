using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

[RequirePermission(Permissions.AdmissionsDischarge)]
public record DischargePatientCommand(
    Guid Id,
    string? Diagnosis,
    string? DischargeSummary,
    string? DischargeCondition
) : IRequest<ApiResponse>;
