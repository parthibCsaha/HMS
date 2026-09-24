using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public record AdmitPatientCommand(
    Guid PatientId,
    Guid AdmittingDoctorId,
    Guid WardId,
    Guid BedId,
    string ReasonForAdmission
) : IRequest<ApiResponse<Guid>>;
