using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Doctors.Commands;
using HMS.Application.Features.Doctors.DTOs;
using HMS.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Doctors.Services;

public class DoctorService(IDoctorRepository doctorRepo, IUserRepository userRepo, IDepartmentRepository deptRepo,
    ICodeGeneratorService codeGen, IUnitOfWork uow, ICurrentUserService currentUser, ILogger<DoctorService> logger) : IDoctorService
{
    public async Task<ApiResponse<PaginatedResponse<DoctorListItemDto>>> GetDoctorsAsync(PaginationQuery query, CancellationToken ct)
    {
        var (items, total) = await doctorRepo.GetPagedAsync(query, ct);
        var dtos = items.Select(d => new DoctorListItemDto(d.Id, d.DoctorCode, d.User.FirstName, d.User.LastName,
            d.User.Email, d.Specialization, d.Department.Name, d.ConsultationFee, d.IsAvailable, d.ExperienceYears));
        return ApiResponse<PaginatedResponse<DoctorListItemDto>>.Success(PaginatedResponse<DoctorListItemDto>.Create(dtos, query.PageNumber, query.PageSize, total));
    }

    public async Task<ApiResponse<DoctorDetailDto>> GetDoctorByIdAsync(Guid id, CancellationToken ct)
    {
        var d = await doctorRepo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Doctor", id);
        if (d.User is null) d.User = (await userRepo.GetByIdAsync(d.UserId, ct))!;
        if (d.Department is null) d.Department = (await deptRepo.GetByIdAsync(d.DepartmentId, ct))!;
        return ApiResponse<DoctorDetailDto>.Success(new DoctorDetailDto(d.Id, d.UserId, d.DoctorCode,
            d.User.FirstName, d.User.LastName, d.User.Email, d.User.Phone,
            d.DepartmentId, d.Department.Name, d.Specialization, d.Qualification, d.LicenseNumber,
            d.ExperienceYears, d.ConsultationFee, d.IsAvailable, d.Biography, d.AvailableDays,
            d.ConsultationStartTime, d.ConsultationEndTime, d.SlotDurationMinutes ?? 30, d.CreatedAt, d.UpdatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateDoctorAsync(CreateDoctorCommand cmd, CancellationToken ct)
    {
        if (!await userRepo.ExistsAsync(cmd.UserId, ct)) throw new NotFoundException("User", cmd.UserId);
        if (!await deptRepo.ExistsAsync(cmd.DepartmentId, ct)) throw new NotFoundException("Department", cmd.DepartmentId);
        var code = await codeGen.GenerateCodeAsync("DOC", ct);
        var doctor = new Doctor
        {
            UserId = cmd.UserId, DoctorCode = code, DepartmentId = cmd.DepartmentId,
            Specialization = cmd.Specialization, Qualification = cmd.Qualification,
            LicenseNumber = cmd.LicenseNumber, ExperienceYears = cmd.ExperienceYears,
            ConsultationFee = cmd.ConsultationFee, IsAvailable = true,
            Biography = cmd.Biography, AvailableDays = cmd.AvailableDays,
            ConsultationStartTime = cmd.ConsultationStartTime, ConsultationEndTime = cmd.ConsultationEndTime,
            SlotDurationMinutes = cmd.SlotDurationMinutes
        };
        await doctorRepo.AddAsync(doctor, ct);
        await uow.SaveChangesAsync(ct);
        logger.LogInformation("Doctor {Code} created", code);
        return ApiResponse<Guid>.Success(doctor.Id, "Doctor created successfully.");
    }

    public async Task<ApiResponse> UpdateDoctorAsync(UpdateDoctorCommand cmd, CancellationToken ct)
    {
        var doctor = await doctorRepo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Doctor", cmd.Id);
        doctor.DepartmentId = cmd.DepartmentId; doctor.Specialization = cmd.Specialization;
        doctor.Qualification = cmd.Qualification; doctor.LicenseNumber = cmd.LicenseNumber;
        doctor.ExperienceYears = cmd.ExperienceYears; doctor.ConsultationFee = cmd.ConsultationFee;
        doctor.IsAvailable = cmd.IsAvailable; doctor.Biography = cmd.Biography;
        doctor.AvailableDays = cmd.AvailableDays; doctor.ConsultationStartTime = cmd.ConsultationStartTime;
        doctor.ConsultationEndTime = cmd.ConsultationEndTime; doctor.SlotDurationMinutes = cmd.SlotDurationMinutes;
        doctorRepo.Update(doctor);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Doctor updated successfully.");
    }

    public async Task<ApiResponse> DeleteDoctorAsync(Guid id, CancellationToken ct)
    {
        var doctor = await doctorRepo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Doctor", id);
        doctorRepo.SoftDelete(doctor, currentUser.UserId);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Doctor deleted successfully.");
    }
}
