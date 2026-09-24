using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.DTOs;
using MediatR;

namespace HMS.Application.Features.Admissions.Queries;

public class GetAdmissionByIdQueryHandler(IAdmissionRecordRepository repo)
    : IRequestHandler<GetAdmissionByIdQuery, ApiResponse<AdmissionDetailDto>>
{
    public async Task<ApiResponse<AdmissionDetailDto>> Handle(
        GetAdmissionByIdQuery request,
        CancellationToken ct
    )
    {
        var admission =
            await repo.GetByIdAsync(request.Id, ct)
            ?? throw new NotFoundException("Admission", request.Id);

        var dto = new AdmissionDetailDto(
            admission.Id,
            admission.AdmissionCode,
            admission.PatientId,
            admission.Patient?.User != null
                ? $"{admission.Patient.User.FirstName} {admission.Patient.User.LastName}"
                : "",
            admission.AdmittingDoctorId,
            admission.AdmittingDoctor?.User != null
                ? $"{admission.AdmittingDoctor.User.FirstName} {admission.AdmittingDoctor.User.LastName}"
                : "",
            admission.WardId,
            admission.Ward?.Name ?? "",
            admission.BedId,
            admission.Bed?.BedNumber ?? "",
            admission.AdmissionDate,
            admission.DischargeDate,
            admission.ReasonForAdmission,
            admission.Diagnosis,
            admission.DischargeSummary,
            admission.DischargeCondition,
            admission.IsActive,
            admission.CreatedAt
        );

        return ApiResponse<AdmissionDetailDto>.Success(dto);
    }
}
