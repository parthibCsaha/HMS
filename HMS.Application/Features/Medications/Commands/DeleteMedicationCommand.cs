using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Medications.Commands;

public record DeleteMedicationCommand(Guid Id) : IRequest<ApiResponse>;
