using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Doctors.Commands;

public class CreateDoctorCommandHandler(
    IDoctorRepository doctorRepo,
    IUserRepository userRepo,
    IDepartmentRepository deptRepo,
    ICodeGeneratorService codeGen,
    IUnitOfWork uow,
    ILogger<CreateDoctorCommandHandler> logger
) : IRequestHandler<CreateDoctorCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateDoctorCommand cmd, CancellationToken ct)
    {
        if (!await userRepo.ExistsAsync(cmd.UserId, ct))
        {
            throw new NotFoundException("User", cmd.UserId);
        }

        if (!await deptRepo.ExistsAsync(cmd.DepartmentId, ct))
        {
            throw new NotFoundException("Department", cmd.DepartmentId);
        }
        var code = await codeGen.GenerateCodeAsync("DOC", ct);
        var doctor = new Doctor
        {
            UserId = cmd.UserId,
            DoctorCode = code,
            DepartmentId = cmd.DepartmentId,
            Specialization = cmd.Specialization,
            Qualification = cmd.Qualification,
            LicenseNumber = cmd.LicenseNumber,
            ExperienceYears = cmd.ExperienceYears,
            ConsultationFee = cmd.ConsultationFee,
            IsAvailable = true,
            Biography = cmd.Biography,
            AvailableDays = cmd.AvailableDays,
            ConsultationStartTime = cmd.ConsultationStartTime,
            ConsultationEndTime = cmd.ConsultationEndTime,
            SlotDurationMinutes = cmd.SlotDurationMinutes,
        };
        await doctorRepo.AddAsync(doctor, ct);
        await uow.SaveChangesAsync(ct);
        logger.LogInformation("Doctor {Code} created", code);
        return ApiResponse<Guid>.Success(doctor.Id, "Doctor created successfully.");
    }
}

