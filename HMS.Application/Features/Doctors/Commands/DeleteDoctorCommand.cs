using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public record DeleteDoctorCommand(Guid Id) : IRequest<ApiResponse>;
