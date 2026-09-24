using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.DTOs;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Queries;

public class GetMedicalRecordByIdQueryHandler(IMedicalRecordRepository repo)
    : IRequestHandler<GetMedicalRecordByIdQuery, ApiResponse<MedicalRecordDetailDto>>
{
    public async Task<ApiResponse<MedicalRecordDetailDto>> Handle(
        GetMedicalRecordByIdQuery request,
        CancellationToken ct
    )
    {
        var record =
            await repo.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("MedicalRecord", request.Id);

        var dto = new MedicalRecordDetailDto(
            record.Id,
            record.RecordCode,
            record.PatientId,
            record.DoctorId,
            record.Doctor?.User != null
                ? $"{record.Doctor.User.FirstName} {record.Doctor.User.LastName}"
                : "",
            record.AppointmentId,
            record.AdmissionId,
            record.VisitDate,
            record.ChiefComplaint,
            record.PresentIllnessHistory,
            record.PastMedicalHistory,
            record.FamilyHistory,
            record.SocialHistory,
            record.ReviewOfSystems,
            record.PhysicalExamination,
            record.Diagnosis,
            record.DifferentialDiagnosis,
            record.Treatment,
            record.Procedures,
            record.Notes,
            record.FollowUpInstructions,
            record.FollowUpDate,
            record.IsConfidential,
            record.CreatedAt
        );

        return ApiResponse<MedicalRecordDetailDto>.Success(dto);
    }
}
