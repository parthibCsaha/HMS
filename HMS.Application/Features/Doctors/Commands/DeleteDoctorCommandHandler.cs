using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Doctors.Commands;

public class DeleteDoctorCommandHandler(
    IDoctorRepository doctorRepo,
    IUnitOfWork uow,
    ICurrentUserService currentUser
) : IRequestHandler<DeleteDoctorCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteDoctorCommand cmd, CancellationToken ct)
    {
        var doctor =
            await doctorRepo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Doctor", cmd.Id);
        doctorRepo.SoftDelete(doctor, currentUser.UserId);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Doctor deleted successfully.");
    }
}
