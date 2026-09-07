using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.DTOs;
using HMS.Application.Features.Prescriptions.Services;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public class GetPrescriptionsByPatientQueryHandler(IPrescriptionService svc) : IRequestHandler<GetPrescriptionsByPatientQuery, ApiResponse<PaginatedResponse<PrescriptionListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<PrescriptionListItemDto>>> Handle(GetPrescriptionsByPatientQuery r, CancellationToken ct)
        => await svc.GetByPatientAsync(r.PatientId, new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize }, ct);
}
