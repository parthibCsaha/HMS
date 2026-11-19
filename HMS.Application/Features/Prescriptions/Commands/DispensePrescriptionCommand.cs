using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

[RequirePermission(Permissions.PrescriptionsDispense)]
public record DispensePrescriptionCommand(Guid Id) : IRequest<ApiResponse>;
