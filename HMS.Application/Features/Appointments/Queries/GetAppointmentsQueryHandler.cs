using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.DTOs;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public class GetAppointmentsQueryHandler(IAppointmentRepository repo)
    : IRequestHandler<GetAppointmentsQuery, ApiResponse<PaginatedResponse<AppointmentListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<AppointmentListItemDto>>> Handle(
        GetAppointmentsQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await repo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize },
            request.DoctorId,
            request.PatientId,
            request.Status,
            request.Date,
            ct
        );

        var dtos = items.Select(appointment => new AppointmentListItemDto(
            appointment.Id,
            appointment.AppointmentCode,
            $"{appointment.Patient.User.FirstName} {appointment.Patient.User.LastName}",
            $"{appointment.Doctor.User.FirstName} {appointment.Doctor.User.LastName}",
            appointment.Department.Name,
            appointment.AppointmentDate,
            appointment.AppointmentTime,
            appointment.DurationMinutes,
            appointment.Type.ToString(),
            appointment.Status.ToString(),
            appointment.IsPaid,
            appointment.ConsultationFee ?? 0m
        ));

        var response = PaginatedResponse<AppointmentListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<AppointmentListItemDto>>.Success(response);
    }
}
