namespace HMS.Application.Features.VitalSigns.DTOs;

public record VitalSignsDto(
    Guid Id,
    Guid PatientId,
    DateTime RecordedAt,
    decimal? TemperatureCelsius,
    int? HeartRateBpm,
    int? RespiratoryRatePerMin,
    string? BloodPressure,
    decimal? OxygenSaturationPercent,
    decimal? WeightKg,
    decimal? HeightCm,
    decimal? BmiValue,
    decimal? BloodGlucoseMgDl,
    string? PainLevel,
    string? Notes
);
