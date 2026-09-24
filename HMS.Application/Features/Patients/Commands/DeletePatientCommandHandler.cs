using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public class DeletePatientCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUser
) : IRequestHandler<DeletePatientCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeletePatientCommand cmd, CancellationToken ct)
    {
        var patient =
            await patientRepository.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException("Patient", cmd.Id);

        patientRepository.SoftDelete(patient, currentUser.UserId);
        await unitOfWork.SaveChangesAsync(ct);

        return ApiResponse.Success("Patient deleted successfully.");
    }
}
