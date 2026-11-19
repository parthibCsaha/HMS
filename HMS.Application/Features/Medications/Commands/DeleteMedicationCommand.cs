using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

[RequirePermission(Permissions.MedicationsDelete)]
public record DeleteMedicationCommand(Guid Id) : IRequest<ApiResponse>;
