using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public record DischargePatientCommand(string? Diagnosis, string? DischargeSummary, string? DischargeCondition) : IRequest<ApiResponse>;
