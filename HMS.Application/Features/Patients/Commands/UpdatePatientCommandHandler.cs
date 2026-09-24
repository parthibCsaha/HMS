using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public class UpdatePatientCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdatePatientCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdatePatientCommand cmd, CancellationToken ct)
    {
        var patient =
            await patientRepository.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException("Patient", cmd.Id);

        patient.DateOfBirth = cmd.DateOfBirth;
        patient.Gender = Enum.Parse<HMS.Domain.Enums.Gender>(cmd.Gender);
        patient.BloodGroup = Enum.Parse<HMS.Domain.Enums.BloodGroup>(cmd.BloodGroup);
        patient.Address = cmd.Address;
        patient.City = cmd.City;
        patient.State = cmd.State;
        patient.Country = cmd.Country;
        patient.PostalCode = cmd.PostalCode;
        patient.EmergencyContactName = cmd.EmergencyContactName;
        patient.EmergencyContactPhone = cmd.EmergencyContactPhone;
        patient.EmergencyContactRelation = cmd.EmergencyContactRelation;
        patient.InsuranceProvider = cmd.InsuranceProvider;
        patient.InsurancePolicyNumber = cmd.InsurancePolicyNumber;
        patient.InsuranceExpiry = cmd.InsuranceExpiry;
        patient.Allergies = cmd.Allergies;
        patient.ChronicConditions = cmd.ChronicConditions;
        patient.Notes = cmd.Notes;

        patientRepository.Update(patient);
        await unitOfWork.SaveChangesAsync(ct);

        return ApiResponse.Success("Patient updated successfully.");
    }
}
