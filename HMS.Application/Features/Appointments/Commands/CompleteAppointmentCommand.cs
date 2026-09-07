using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public record CompleteAppointmentCommand(Guid Id) : IRequest<ApiResponse>;
