using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Commands;

public record UpdateMedicalRecordCommand(
    Guid Id,
    string? ChiefComplaint,
    string Diagnosis,
    string? Treatment,
    string? Notes,
    string? FollowUpInstructions,
    DateTime? FollowUpDate
) : IRequest<ApiResponse>;
