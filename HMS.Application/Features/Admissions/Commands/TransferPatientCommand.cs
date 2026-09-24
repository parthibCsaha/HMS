using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public record TransferPatientCommand(Guid Id, Guid NewWardId, Guid NewBedId) : IRequest<ApiResponse>;
