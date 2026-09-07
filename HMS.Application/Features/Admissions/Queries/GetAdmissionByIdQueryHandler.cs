using HMS.Application.Common.Models;
using HMS.Application.Features.Admissions.DTOs;
using HMS.Application.Features.Admissions.Services;
using MediatR;

namespace HMS.Application.Features.Admissions.Queries;

public class GetAdmissionByIdQueryHandler(IAdmissionService svc) : IRequestHandler<GetAdmissionByIdQuery, ApiResponse<AdmissionDetailDto>>
{
    public async Task<ApiResponse<AdmissionDetailDto>> Handle(GetAdmissionByIdQuery r, CancellationToken ct)
        => await svc.GetByIdAsync(r.Id, ct);
}
