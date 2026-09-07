using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.DTOs;
using HMS.Application.Features.Appointments.Services;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public class GetAppointmentsQueryHandler(IAppointmentService svc) : IRequestHandler<GetAppointmentsQuery, ApiResponse<PaginatedResponse<AppointmentListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<AppointmentListItemDto>>> Handle(GetAppointmentsQuery r, CancellationToken ct)
        => await svc.GetAppointmentsAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize, SearchTerm = r.SearchTerm }, r.DoctorId, r.PatientId, r.Status, r.Date, ct);
}
