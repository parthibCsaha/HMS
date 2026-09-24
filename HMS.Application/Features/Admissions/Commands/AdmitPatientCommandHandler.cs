using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public class AdmitPatientCommandHandler(
    IAdmissionRecordRepository repo,
    IPatientRepository patientRepo,
    IBedRepository bedRepo,
    ICodeGeneratorService codeGen,
    IUnitOfWork uow
) : IRequestHandler<AdmitPatientCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(AdmitPatientCommand cmd, CancellationToken ct)
    {
        var existing = await repo.GetActiveByPatientAsync(cmd.PatientId, ct);
        if (existing is not null)
            throw new ConflictException("Patient is already admitted.");
        var bed =
            await bedRepo.GetByIdAsync(cmd.BedId, ct)
            ?? throw new NotFoundException("Bed", cmd.BedId);
        if (bed.Status != BedStatus.Available)
            throw new BadRequestException("Bed is not available.");
        var code = await codeGen.GenerateCodeAsync("ADM", ct);
        var record = new AdmissionRecord
        {
            AdmissionCode = code,
            PatientId = cmd.PatientId,
            AdmittingDoctorId = cmd.AdmittingDoctorId,
            WardId = cmd.WardId,
            BedId = cmd.BedId,
            AdmissionDate = DateTime.UtcNow,
            ReasonForAdmission = cmd.ReasonForAdmission,
            IsActive = true,
        };
        bed.Status = BedStatus.Occupied;
        bed.CurrentPatientId = cmd.PatientId;
        bed.OccupiedAt = DateTime.UtcNow;
        bedRepo.Update(bed);

        var patient = await patientRepo.GetByIdAsync(cmd.PatientId, ct);
        if (patient != null)
        {
            patient.IsAdmitted = true;
            patientRepo.Update(patient);
        }

        await repo.AddAsync(record, ct);
        await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(record.Id, "Patient admitted.");
    }
}

