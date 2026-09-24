using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.DTOs;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public class GetDoctorsQueryHandler(IDoctorRepository doctorRepo)
    : IRequestHandler<GetDoctorsQuery, ApiResponse<PaginatedResponse<DoctorListItemDto>>>
{
    public async Task<ApiResponse<PaginatedResponse<DoctorListItemDto>>> Handle(
        GetDoctorsQuery request,
        CancellationToken ct
    )
    {
        var (items, total) = await doctorRepo.GetPagedAsync(
            new PaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, SearchTerm = request.SearchTerm },
            ct
        );

        var dtos = items.Select(doctor => new DoctorListItemDto(
            doctor.Id,
            doctor.DoctorCode,
            doctor.User.FirstName,
            doctor.User.LastName,
            doctor.User.Email,
            doctor.Specialization,
            doctor.Department.Name,
            doctor.ConsultationFee,
            doctor.IsAvailable,
            doctor.ExperienceYears
        ));

        var response = PaginatedResponse<DoctorListItemDto>.Create(
            dtos,
            request.PageNumber,
            request.PageSize,
            total
        );
        return ApiResponse<PaginatedResponse<DoctorListItemDto>>.Success(response);
    }
}
