using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.VitalSigns.Commands;

public class RecordVitalSignsCommandHandler(IVitalSignsRepository repo, IUnitOfWork uow)
    : IRequestHandler<RecordVitalSignsCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(RecordVitalSignsCommand cmd, CancellationToken ct)
    {
        var bmi = (cmd.WeightKg.HasValue && cmd.HeightCm.HasValue && cmd.HeightCm > 0)
            ? Math.Round(cmd.WeightKg.Value / ((cmd.HeightCm.Value / 100m) * (cmd.HeightCm.Value / 100m)), 2) : cmd.BloodGlucoseMgDl;
        var vs = new Domain.Entities.VitalSigns
        {
            PatientId = cmd.PatientId, AppointmentId = cmd.AppointmentId, AdmissionId = cmd.AdmissionId,
            RecordedAt = DateTime.UtcNow, TemperatureCelsius = cmd.TemperatureCelsius,
            HeartRateBpm = cmd.HeartRateBpm, RespiratoryRatePerMin = cmd.RespiratoryRatePerMin,
            BloodPressure = cmd.BloodPressure, OxygenSaturationPercent = cmd.OxygenSaturationPercent,
            WeightKg = cmd.WeightKg, HeightCm = cmd.HeightCm, BmiValue = bmi,
            BloodGlucoseMgDl = cmd.BloodGlucoseMgDl, PainLevel = cmd.PainLevel, Notes = cmd.Notes
        };
        await repo.AddAsync(vs, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(vs.Id, "Vital signs recorded.");
    }
}
