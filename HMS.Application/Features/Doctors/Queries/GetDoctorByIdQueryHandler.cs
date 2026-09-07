using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.DTOs;
using HMS.Application.Features.Doctors.Services;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public class GetDoctorByIdQueryHandler(IDoctorService svc) : IRequestHandler<GetDoctorByIdQuery, ApiResponse<DoctorDetailDto>>
{
    public async Task<ApiResponse<DoctorDetailDto>> Handle(GetDoctorByIdQuery r, CancellationToken ct)
        => await svc.GetDoctorByIdAsync(r.Id, ct);
}
