using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.DTOs;
using HMS.Application.Features.Prescriptions.Services;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Queries;

public class GetPrescriptionByIdQueryHandler(IPrescriptionService svc) : IRequestHandler<GetPrescriptionByIdQuery, ApiResponse<PrescriptionDetailDto>>
{
    public async Task<ApiResponse<PrescriptionDetailDto>> Handle(GetPrescriptionByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
