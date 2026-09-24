using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.DTOs;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public record GetAppointmentsQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    Guid? DoctorId = null,
    Guid? PatientId = null,
    AppointmentStatus? Status = null,
    DateTime? Date = null
) : IRequest<ApiResponse<PaginatedResponse<AppointmentListItemDto>>>;
