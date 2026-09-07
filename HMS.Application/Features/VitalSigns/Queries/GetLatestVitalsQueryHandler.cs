using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.VitalSigns.DTOs;
using MediatR;

namespace HMS.Application.Features.VitalSigns.Queries;

public class GetLatestVitalsQueryHandler(IVitalSignsRepository repo) : IRequestHandler<GetLatestVitalsQuery, ApiResponse<VitalSignsDto?>>
{
    public async Task<ApiResponse<VitalSignsDto?>> Handle(GetLatestVitalsQuery r, CancellationToken ct)
    {
        var v = await repo.GetLatestByPatientAsync(r.PatientId, ct);
        if (v is null) return ApiResponse<VitalSignsDto?>.Success(null, "No vital signs found.");
        return ApiResponse<VitalSignsDto?>.Success(new VitalSignsDto(v.Id, v.PatientId, v.RecordedAt,
            v.TemperatureCelsius, v.HeartRateBpm, v.RespiratoryRatePerMin, v.BloodPressure,
            v.OxygenSaturationPercent, v.WeightKg, v.HeightCm, v.BmiValue, v.BloodGlucoseMgDl, v.PainLevel, v.Notes));
    }
}
