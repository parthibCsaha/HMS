using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

[RequirePermission(Permissions.AdmissionsTransfer)]
public record TransferPatientCommand(Guid Id, Guid NewWardId, Guid NewBedId) : IRequest<ApiResponse>;
