using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.Commands;
using HMS.Application.Features.MedicalRecords.DTOs;
using HMS.Domain.Entities;

namespace HMS.Application.Features.MedicalRecords.Services;

public class MedicalRecordService(IMedicalRecordRepository repo, ICodeGeneratorService codeGen, IUnitOfWork uow) : IMedicalRecordService
{
    public async Task<ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>> GetByPatientAsync(Guid patientId, PaginationQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedByPatientAsync(patientId, q, ct);
        var dtos = items.Select(mr => new MedicalRecordListItemDto(mr.Id, mr.RecordCode,
            mr.Doctor?.User != null ? $"{mr.Doctor.User.FirstName} {mr.Doctor.User.LastName}" : "",
            mr.VisitDate, mr.Diagnosis, mr.ChiefComplaint, mr.IsConfidential));
        return ApiResponse<PaginatedResponse<MedicalRecordListItemDto>>.Success(PaginatedResponse<MedicalRecordListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<MedicalRecordDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var mr = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("MedicalRecord", id);
        return ApiResponse<MedicalRecordDetailDto>.Success(new MedicalRecordDetailDto(mr.Id, mr.RecordCode,
            mr.PatientId, mr.DoctorId, mr.Doctor?.User != null ? $"{mr.Doctor.User.FirstName} {mr.Doctor.User.LastName}" : "",
            mr.AppointmentId, mr.AdmissionId, mr.VisitDate, mr.ChiefComplaint, mr.PresentIllnessHistory,
            mr.PastMedicalHistory, mr.FamilyHistory, mr.SocialHistory, mr.ReviewOfSystems, mr.PhysicalExamination,
            mr.Diagnosis, mr.DifferentialDiagnosis, mr.Treatment, mr.Procedures, mr.Notes,
            mr.FollowUpInstructions, mr.FollowUpDate, mr.IsConfidential, mr.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateMedicalRecordCommand cmd, CancellationToken ct)
    {
        var code = await codeGen.GenerateCodeAsync("MR", ct);
        var record = new MedicalRecord
        {
            RecordCode = code, PatientId = cmd.PatientId, DoctorId = cmd.DoctorId,
            AppointmentId = cmd.AppointmentId, AdmissionId = cmd.AdmissionId, VisitDate = cmd.VisitDate,
            ChiefComplaint = cmd.ChiefComplaint, PresentIllnessHistory = cmd.PresentIllnessHistory,
            PastMedicalHistory = cmd.PastMedicalHistory, FamilyHistory = cmd.FamilyHistory,
            SocialHistory = cmd.SocialHistory, ReviewOfSystems = cmd.ReviewOfSystems,
            PhysicalExamination = cmd.PhysicalExamination, Diagnosis = cmd.Diagnosis,
            DifferentialDiagnosis = cmd.DifferentialDiagnosis, Treatment = cmd.Treatment,
            Procedures = cmd.Procedures, Notes = cmd.Notes, FollowUpInstructions = cmd.FollowUpInstructions,
            FollowUpDate = cmd.FollowUpDate, IsConfidential = cmd.IsConfidential
        };
        await repo.AddAsync(record, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(record.Id, "Medical record created.");
    }

    public async Task<ApiResponse> UpdateAsync(UpdateMedicalRecordCommand cmd, CancellationToken ct)
    {
        var mr = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("MedicalRecord", cmd.Id);
        mr.ChiefComplaint = cmd.ChiefComplaint; mr.Diagnosis = cmd.Diagnosis; mr.Treatment = cmd.Treatment;
        mr.Notes = cmd.Notes; mr.FollowUpInstructions = cmd.FollowUpInstructions; mr.FollowUpDate = cmd.FollowUpDate;
        repo.Update(mr); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Medical record updated.");
    }
}
