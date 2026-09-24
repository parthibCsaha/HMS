using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.VitalSigns.Commands;

public record RecordVitalSignsCommand(
    Guid PatientId,
    Guid? AppointmentId,
    Guid? AdmissionId,
    decimal? TemperatureCelsius,
    int? HeartRateBpm,
    int? RespiratoryRatePerMin,
    string? BloodPressure,
    decimal? OxygenSaturationPercent,
    decimal? WeightKg,
    decimal? HeightCm,
    decimal? BloodGlucoseMgDl,
    string? PainLevel,
    string? Notes
) : IRequest<ApiResponse<Guid>>;
