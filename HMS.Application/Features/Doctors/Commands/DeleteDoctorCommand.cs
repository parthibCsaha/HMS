using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

[RequirePermission(Permissions.DoctorsDelete)]
public record DeleteDoctorCommand(Guid Id) : IRequest<ApiResponse>;
