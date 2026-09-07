using HMS.Application.Common.Models;
using HMS.Application.Features.VitalSigns.DTOs;
using MediatR;

namespace HMS.Application.Features.VitalSigns.Queries;

public record GetVitalSignsQuery(Guid PatientId, DateTime? From = null, DateTime? To = null)
    : IRequest<ApiResponse<IEnumerable<VitalSignsDto>>>;
