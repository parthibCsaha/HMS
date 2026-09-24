using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.DTOs;
using MediatR;

namespace HMS.Application.Features.Doctors.Queries;

public class GetDoctorByIdQueryHandler(
    IDoctorRepository doctorRepo,
    IUserRepository userRepo,
    IDepartmentRepository deptRepo
) : IRequestHandler<GetDoctorByIdQuery, ApiResponse<DoctorDetailDto>>
{
    public async Task<ApiResponse<DoctorDetailDto>> Handle(
        GetDoctorByIdQuery request,
        CancellationToken ct
    )
    {
        var doctor = await doctorRepo.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("Doctor", request.Id);

        if (doctor.User is null)
        {
            doctor.User = (await userRepo.GetByIdAsync(doctor.UserId, ct))!;
        }

        if (doctor.Department is null)
        {
            doctor.Department = (await deptRepo.GetByIdAsync(doctor.DepartmentId, ct))!;
        }

        var dto = new DoctorDetailDto(
            doctor.Id,
            doctor.UserId,
            doctor.DoctorCode,
            doctor.User.FirstName,
            doctor.User.LastName,
            doctor.User.Email,
            doctor.User.Phone,
            doctor.DepartmentId,
            doctor.Department.Name,
            doctor.Specialization,
            doctor.Qualification,
            doctor.LicenseNumber,
            doctor.ExperienceYears,
            doctor.ConsultationFee,
            doctor.IsAvailable,
            doctor.Biography,
            doctor.AvailableDays,
            doctor.ConsultationStartTime,
            doctor.ConsultationEndTime,
            doctor.SlotDurationMinutes ?? 30,
            doctor.CreatedAt,
            doctor.UpdatedAt
        );

        return ApiResponse<DoctorDetailDto>.Success(dto);
    }
}
