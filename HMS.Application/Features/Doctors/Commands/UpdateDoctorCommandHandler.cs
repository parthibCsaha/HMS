using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class UpdateDoctorCommandHandler(
    IDoctorRepository doctorRepo,
    IUnitOfWork uow
) : IRequestHandler<UpdateDoctorCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateDoctorCommand cmd, CancellationToken ct)
    {
        var doctor =
            await doctorRepo.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException("Doctor", cmd.Id);

        doctor.DepartmentId = cmd.DepartmentId;
        doctor.Specialization = cmd.Specialization;
        doctor.Qualification = cmd.Qualification;
        doctor.LicenseNumber = cmd.LicenseNumber;
        doctor.ExperienceYears = cmd.ExperienceYears;
        doctor.ConsultationFee = cmd.ConsultationFee;
        doctor.IsAvailable = cmd.IsAvailable;
        doctor.Biography = cmd.Biography;
        doctor.AvailableDays = cmd.AvailableDays;
        doctor.ConsultationStartTime = cmd.ConsultationStartTime;
        doctor.ConsultationEndTime = cmd.ConsultationEndTime;
        doctor.SlotDurationMinutes = cmd.SlotDurationMinutes;

        doctorRepo.Update(doctor);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Doctor updated successfully.");
    }
}
