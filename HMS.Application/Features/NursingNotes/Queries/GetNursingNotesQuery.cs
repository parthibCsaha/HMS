using HMS.Application.Common.Models;
using HMS.Application.Features.NursingNotes.DTOs;
using MediatR;

namespace HMS.Application.Features.NursingNotes.Queries;

public record GetNursingNotesQuery(
    int PageNumber = 1,
    int PageSize = 10,
    Guid? PatientId = null,
    Guid? AdmissionId = null
) : IRequest<ApiResponse<PaginatedResponse<NursingNoteDto>>>;
