using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

[RequirePermission(Permissions.AppointmentsCheckIn)]
public record CheckInAppointmentCommand(Guid Id) : IRequest<ApiResponse>;
