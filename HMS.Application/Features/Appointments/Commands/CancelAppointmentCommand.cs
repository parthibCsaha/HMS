using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

[RequirePermission(Permissions.AppointmentsCancel)]
public record CancelAppointmentCommand(Guid Id, string Reason) : IRequest<ApiResponse>;
