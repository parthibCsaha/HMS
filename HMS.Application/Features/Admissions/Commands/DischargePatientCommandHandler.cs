using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public class DischargePatientCommandHandler(
    IAdmissionRecordRepository repo,
    IPatientRepository patientRepo,
    IBedRepository bedRepo,
    IUnitOfWork uow
) : IRequestHandler<DischargePatientCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DischargePatientCommand cmd, CancellationToken ct)
    {
        var record =
            await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Admission", cmd.Id);

        record.DischargeDate = DateTime.UtcNow;
        record.Diagnosis = cmd.Diagnosis;
        record.DischargeSummary = cmd.DischargeSummary;
        record.DischargeCondition = cmd.DischargeCondition;
        record.IsActive = false;

        repo.Update(record);

        var bed = await bedRepo.GetByIdAsync(record.BedId, ct);
        if (bed != null)
        {
            bed.Status = BedStatus.Available;
            bed.CurrentPatientId = null;
            bed.OccupiedAt = null;
            bedRepo.Update(bed);
        }

        var patient = await patientRepo.GetByIdAsync(record.PatientId, ct);
        if (patient != null)
        {
            patient.IsAdmitted = false;
            patientRepo.Update(patient);
        }
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Patient discharged.");
    }
}

