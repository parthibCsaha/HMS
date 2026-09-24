using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public record DischargePatientCommand(
    Guid Id,
    string? Diagnosis,
    string? DischargeSummary,
    string? DischargeCondition
) : IRequest<ApiResponse>;
