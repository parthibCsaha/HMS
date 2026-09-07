using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.NursingNotes.DTOs;
using MediatR;

namespace HMS.Application.Features.NursingNotes.Queries;

public class GetNursingNotesQueryHandler(INursingNoteRepository repo) : IRequestHandler<GetNursingNotesQuery, ApiResponse<PaginatedResponse<NursingNoteDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<NursingNoteDto>>> Handle(GetNursingNotesQuery r, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize }, r.PatientId, r.AdmissionId, ct);
        var dtos = items.Select(n => new NursingNoteDto(n.Id, n.PatientId,
            n.Patient?.User != null ? $"{n.Patient.User.FirstName} {n.Patient.User.LastName}" : "",
            n.NoteType, n.Note, n.ActionTaken, n.NoteDateTime));
        return ApiResponse<PaginatedResponse<NursingNoteDto>>.Success(PaginatedResponse<NursingNoteDto>.Create(dtos, r.PageNumber, r.PageSize, total));
    }
}
