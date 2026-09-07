using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Appointments.Commands;

public record CancelAppointmentCommand(Guid Id, string Reason) : IRequest<ApiResponse>;
