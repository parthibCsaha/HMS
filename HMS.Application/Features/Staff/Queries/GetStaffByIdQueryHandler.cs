using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.DTOs;
using HMS.Application.Features.Staff.Services;
using MediatR;

namespace HMS.Application.Features.Staff.Queries;

public class GetStaffByIdQueryHandler(IStaffService svc) : IRequestHandler<GetStaffByIdQuery, ApiResponse<StaffDetailDto>>
{
    public async Task<ApiResponse<StaffDetailDto>> Handle(GetStaffByIdQuery r, CancellationToken ct)
        => await svc.GetStaffByIdAsync(r.Id, ct);
}
