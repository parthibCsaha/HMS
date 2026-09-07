using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public record CheckInAppointmentCommand(Guid Id) : IRequest<ApiResponse>;
