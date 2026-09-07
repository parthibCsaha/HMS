using HMS.Application.Common.Models;
using HMS.Application.Features.Appointments.DTOs;
using MediatR;

namespace HMS.Application.Features.Appointments.Queries;

public record GetAppointmentByIdQuery(Guid Id) : IRequest<ApiResponse<AppointmentDetailDto>>;
