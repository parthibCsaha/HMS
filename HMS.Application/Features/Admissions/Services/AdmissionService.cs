using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.Commands;
using HMS.Application.Features.Admissions.DTOs;
using HMS.Domain.Entities;
using HMS.Domain.Enums;

namespace HMS.Application.Features.Admissions.Services;

public class AdmissionService(
    IAdmissionRecordRepository repo, 
    IPatientRepository patientRepo,
    IBedRepository bedRepo, 
    ICodeGeneratorService codeGen, 
    IUnitOfWork uow) : IAdmissionService
{
    public async Task<ApiResponse<PaginatedResponse<AdmissionListItemDto>>> GetAdmissionsAsync(PaginationQuery q, Guid? patientId, bool? isActive, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(q, patientId, isActive, ct);

        var dtos = items.Select(a => new AdmissionListItemDto(a.Id, a.AdmissionCode,
            $"{a.Patient.User.FirstName} {a.Patient.User.LastName}",
            $"{a.AdmittingDoctor.User.FirstName} {a.AdmittingDoctor.User.LastName}",
            a.Ward.Name, a.Bed.BedNumber, a.AdmissionDate, a.IsActive));

        return ApiResponse<PaginatedResponse<AdmissionListItemDto>>.Success(
            PaginatedResponse<AdmissionListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<AdmissionDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var a = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Admission", id);
        return ApiResponse<AdmissionDetailDto>.Success(new AdmissionDetailDto(a.Id, a.AdmissionCode,
            a.PatientId, a.Patient?.User != null ? $"{a.Patient.User.FirstName} {a.Patient.User.LastName}" : "",
            a.AdmittingDoctorId, a.AdmittingDoctor?.User != null ? $"{a.AdmittingDoctor.User.FirstName} {a.AdmittingDoctor.User.LastName}" : "",
            a.WardId, a.Ward?.Name ?? "", a.BedId, a.Bed?.BedNumber ?? "",
            a.AdmissionDate, a.DischargeDate, a.ReasonForAdmission, a.Diagnosis,
            a.DischargeSummary, a.DischargeCondition, a.IsActive, a.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> AdmitAsync(AdmitPatientCommand cmd, CancellationToken ct)
    {
        var existing = await repo.GetActiveByPatientAsync(cmd.PatientId, ct);
        if (existing is not null) throw new ConflictException("Patient is already admitted.");
        var bed = await bedRepo.GetByIdAsync(cmd.BedId, ct) ?? throw new NotFoundException("Bed", cmd.BedId);
        if (bed.Status != BedStatus.Available) throw new BadRequestException("Bed is not available.");
        var code = await codeGen.GenerateCodeAsync("ADM", ct);
        var record = new AdmissionRecord
        {
            AdmissionCode = code, PatientId = cmd.PatientId, AdmittingDoctorId = cmd.AdmittingDoctorId,
            WardId = cmd.WardId, BedId = cmd.BedId, AdmissionDate = DateTime.UtcNow,
            ReasonForAdmission = cmd.ReasonForAdmission, IsActive = true
        };
        bed.Status = BedStatus.Occupied; bed.CurrentPatientId = cmd.PatientId; bed.OccupiedAt = DateTime.UtcNow;
        bedRepo.Update(bed);
        var patient = await patientRepo.GetByIdAsync(cmd.PatientId, ct);
        if (patient != null) { patient.IsAdmitted = true; patientRepo.Update(patient); }
        await repo.AddAsync(record, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(record.Id, "Patient admitted.");
    }

    public async Task<ApiResponse> DischargeAsync(Guid id, DischargePatientCommand cmd, CancellationToken ct)
    {
        var record = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Admission", id);
        record.DischargeDate = DateTime.UtcNow; record.Diagnosis = cmd.Diagnosis;
        record.DischargeSummary = cmd.DischargeSummary; record.DischargeCondition = cmd.DischargeCondition; record.IsActive = false;
        repo.Update(record);
        var bed = await bedRepo.GetByIdAsync(record.BedId, ct);
        if (bed != null) { bed.Status = BedStatus.Available; bed.CurrentPatientId = null; bed.OccupiedAt = null; bedRepo.Update(bed); }
        var patient = await patientRepo.GetByIdAsync(record.PatientId, ct);
        if (patient != null) { patient.IsAdmitted = false; patientRepo.Update(patient); }
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Patient discharged.");
    }

    public async Task<ApiResponse> TransferAsync(Guid id, TransferPatientCommand cmd, CancellationToken ct)
    {
        var record = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Admission", id);
        var oldBed = await bedRepo.GetByIdAsync(record.BedId, ct);
        if (oldBed != null) { oldBed.Status = BedStatus.Available; oldBed.CurrentPatientId = null; bedRepo.Update(oldBed); }
        var newBed = await bedRepo.GetByIdAsync(cmd.NewBedId, ct) ?? throw new NotFoundException("Bed", cmd.NewBedId);
        if (newBed.Status != BedStatus.Available) throw new BadRequestException("New bed is not available.");
        newBed.Status = BedStatus.Occupied; newBed.CurrentPatientId = record.PatientId; newBed.OccupiedAt = DateTime.UtcNow;
        bedRepo.Update(newBed);
        record.WardId = cmd.NewWardId; record.BedId = cmd.NewBedId; repo.Update(record);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Patient transferred.");
    }
}
