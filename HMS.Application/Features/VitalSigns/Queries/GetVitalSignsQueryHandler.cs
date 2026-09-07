using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.VitalSigns.DTOs;
using MediatR;

namespace HMS.Application.Features.VitalSigns.Queries;

public class GetVitalSignsQueryHandler(IVitalSignsRepository repo) : IRequestHandler<GetVitalSignsQuery, ApiResponse<IEnumerable<VitalSignsDto>>>
{
    public async Task<ApiResponse<IEnumerable<VitalSignsDto>>> Handle(GetVitalSignsQuery r, CancellationToken ct)
    {
        var items = await repo.GetByPatientAsync(r.PatientId, r.From, r.To, ct);
        var dtos = items.Select(v => new VitalSignsDto(v.Id, v.PatientId, v.RecordedAt, v.TemperatureCelsius,
            v.HeartRateBpm, v.RespiratoryRatePerMin, v.BloodPressure, v.OxygenSaturationPercent,
            v.WeightKg, v.HeightCm, v.BmiValue, v.BloodGlucoseMgDl, v.PainLevel, v.Notes));
        return ApiResponse<IEnumerable<VitalSignsDto>>.Success(dtos);
    }
}
