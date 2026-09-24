using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Commands;

public class CreateMedicalRecordCommandHandler(
    IMedicalRecordRepository repo,
    HMS.Application.Common.Interfaces.Services.ICodeGeneratorService codeGen,
    IUnitOfWork uow
) : IRequestHandler<CreateMedicalRecordCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateMedicalRecordCommand cmd, CancellationToken ct)
    {
        var code = await codeGen.GenerateCodeAsync("MR", ct);
        var record = new MedicalRecord
        {
            RecordCode = code,
            PatientId = cmd.PatientId,
            DoctorId = cmd.DoctorId,
            AppointmentId = cmd.AppointmentId,
            AdmissionId = cmd.AdmissionId,
            VisitDate = cmd.VisitDate,
            ChiefComplaint = cmd.ChiefComplaint,
            PresentIllnessHistory = cmd.PresentIllnessHistory,
            PastMedicalHistory = cmd.PastMedicalHistory,
            FamilyHistory = cmd.FamilyHistory,
            SocialHistory = cmd.SocialHistory,
            ReviewOfSystems = cmd.ReviewOfSystems,
            PhysicalExamination = cmd.PhysicalExamination,
            Diagnosis = cmd.Diagnosis,
            DifferentialDiagnosis = cmd.DifferentialDiagnosis,
            Treatment = cmd.Treatment,
            Procedures = cmd.Procedures,
            Notes = cmd.Notes,
            FollowUpInstructions = cmd.FollowUpInstructions,
            FollowUpDate = cmd.FollowUpDate,
            IsConfidential = cmd.IsConfidential,
        };
        await repo.AddAsync(record, ct);
        await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(record.Id, "Medical record created.");
    }
}
