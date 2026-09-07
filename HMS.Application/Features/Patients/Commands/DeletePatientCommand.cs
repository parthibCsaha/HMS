using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public record DeletePatientCommand(Guid Id) : IRequest<ApiResponse>;
