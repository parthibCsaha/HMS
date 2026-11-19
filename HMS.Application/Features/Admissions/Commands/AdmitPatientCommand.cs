using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

[RequirePermission(Permissions.AdmissionsAdmit)]
public record AdmitPatientCommand(
    Guid PatientId,
    Guid AdmittingDoctorId,
    Guid WardId,
    Guid BedId,
    string ReasonForAdmission
) : IRequest<ApiResponse<Guid>>;
