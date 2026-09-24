using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.DTOs;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public class GetAppointmentByIdQueryHandler(
    IAppointmentRepository repo,
    IPatientRepository patientRepo,
    IDoctorRepository doctorRepo
) : IRequestHandler<GetAppointmentByIdQuery, ApiResponse<AppointmentDetailDto>>
{
    public async Task<ApiResponse<AppointmentDetailDto>> Handle(
        GetAppointmentByIdQuery request,
        CancellationToken ct
    )
    {
        var appointment =
            await repo.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("Appointment", request.Id);

        if (appointment.Patient?.User is null)
        {
            var patient = await patientRepo.GetByIdAsync(appointment.PatientId, ct);
            appointment.Patient = patient!;
        }

        if (appointment.Doctor?.User is null)
        {
            var doctor = await doctorRepo.GetByIdAsync(appointment.DoctorId, ct);
            appointment.Doctor = doctor!;
        }

        var dto = new AppointmentDetailDto(
            appointment.Id,
            appointment.AppointmentCode,
            appointment.PatientId,
            $"{appointment.Patient.User.FirstName} {appointment.Patient.User.LastName}",
            appointment.DoctorId,
            $"{appointment.Doctor.User.FirstName} {appointment.Doctor.User.LastName}",
            appointment.DepartmentId,
            appointment.Department?.Name ?? "",
            appointment.AppointmentDate,
            appointment.AppointmentTime,
            appointment.DurationMinutes,
            appointment.Type.ToString(),
            appointment.Status.ToString(),
            appointment.ChiefComplaint,
            appointment.Notes,
            appointment.CancellationReason,
            appointment.IsPaid,
            appointment.ConsultationFee ?? 0m,
            appointment.ReferredByDoctorId,
            appointment.CheckedInAt,
            appointment.CompletedAt,
            appointment.CreatedAt
        );

        return ApiResponse<AppointmentDetailDto>.Success(dto);
    }
}
