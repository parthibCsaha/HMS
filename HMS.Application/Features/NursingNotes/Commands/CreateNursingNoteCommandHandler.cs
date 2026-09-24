using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.NursingNotes.Commands;

public class CreateNursingNoteCommandHandler(INursingNoteRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateNursingNoteCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateNursingNoteCommand cmd, CancellationToken ct)
    {
        var note = new NursingNote
        {
            PatientId = cmd.PatientId,
            NurseId = cmd.NurseId,
            AdmissionId = cmd.AdmissionId,
            WardId = cmd.WardId,
            BedId = cmd.BedId,
            NoteType = cmd.NoteType,
            Note = cmd.Note,
            ActionTaken = cmd.ActionTaken,
            NoteDateTime = DateTime.UtcNow,
        };
        await repo.AddAsync(note, ct);
        await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(note.Id, "Nursing note created.");
    }
}
