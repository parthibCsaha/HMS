using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public record DispensePrescriptionCommand(Guid Id) : IRequest<ApiResponse>;
