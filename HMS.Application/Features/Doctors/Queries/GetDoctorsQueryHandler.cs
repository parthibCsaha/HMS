using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.DTOs;
using HMS.Application.Features.Doctors.Services;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public class GetDoctorsQueryHandler(IDoctorService svc) : IRequestHandler<GetDoctorsQuery, ApiResponse<PaginatedResponse<DoctorListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<DoctorListItemDto>>> Handle(GetDoctorsQuery r, CancellationToken ct)
        => await svc.GetDoctorsAsync(new PaginationQuery { PageNumber = r.PageNumber, PageSize = r.PageSize, SearchTerm = r.SearchTerm, SortBy = r.SortBy, IsDescending = r.IsDescending }, ct);
}
