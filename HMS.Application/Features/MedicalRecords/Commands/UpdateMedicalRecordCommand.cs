using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Commands;

[RequirePermission(Permissions.MedicalRecordsUpdate)]
public record UpdateMedicalRecordCommand(
    Guid Id,
    string? ChiefComplaint,
    string Diagnosis,
    string? Treatment,
    string? Notes,
    string? FollowUpInstructions,
    DateTime? FollowUpDate
) : IRequest<ApiResponse>;
