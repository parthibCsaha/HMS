using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.NursingNotes.Commands;

public record CreateNursingNoteCommand(
    Guid PatientId,
    Guid NurseId,
    Guid? AdmissionId,
    Guid? WardId,
    Guid? BedId,
    string NoteType,
    string Note,
    string? ActionTaken
) : IRequest<ApiResponse<Guid>>;
