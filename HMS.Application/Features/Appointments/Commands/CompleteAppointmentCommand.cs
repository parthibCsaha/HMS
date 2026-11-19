using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

[RequirePermission(Permissions.AppointmentsComplete)]
public record CompleteAppointmentCommand(Guid Id) : IRequest<ApiResponse>;
