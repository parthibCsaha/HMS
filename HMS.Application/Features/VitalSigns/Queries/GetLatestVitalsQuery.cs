using HMS.Application.Common.Models;
using HMS.Application.Features.VitalSigns.DTOs;
using MediatR;

namespace HMS.Application.Features.VitalSigns.Queries;

public record GetLatestVitalsQuery(Guid PatientId) : IRequest<ApiResponse<VitalSignsDto?>>;
