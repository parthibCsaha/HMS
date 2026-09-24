namespace HMS.Application.Features.NursingNotes.DTOs;

public record NursingNoteDto(
    Guid Id,
    Guid PatientId,
    string PatientName,
    string NoteType,
    string Note,
    string? ActionTaken,
    DateTime NoteDateTime
);
