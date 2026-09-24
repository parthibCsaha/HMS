using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Patients.Commands;

public class CreatePatientCommandHandler(
    IPatientRepository patientRepository,
    IUserRepository userRepository,
    ICodeGeneratorService codeGenerator,
    IUnitOfWork unitOfWork,
    ILogger<CreatePatientCommandHandler> logger
) : IRequestHandler<CreatePatientCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreatePatientCommand cmd, CancellationToken ct)
    {
        if (!await userRepository.ExistsAsync(cmd.UserId, ct))
            throw new NotFoundException("User", cmd.UserId);

        var code = await codeGenerator.GenerateCodeAsync("PAT", ct);

        var patient = new Patient
        {
            UserId = cmd.UserId,
            PatientCode = code,
            DateOfBirth = cmd.DateOfBirth,
            Gender = Enum.Parse<HMS.Domain.Enums.Gender>(cmd.Gender),
            BloodGroup = Enum.Parse<HMS.Domain.Enums.BloodGroup>(cmd.BloodGroup),
            Address = cmd.Address,
            City = cmd.City,
            State = cmd.State,
            Country = cmd.Country,
            PostalCode = cmd.PostalCode,
            EmergencyContactName = cmd.EmergencyContactName,
            EmergencyContactPhone = cmd.EmergencyContactPhone,
            EmergencyContactRelation = cmd.EmergencyContactRelation,
            InsuranceProvider = cmd.InsuranceProvider,
            InsurancePolicyNumber = cmd.InsurancePolicyNumber,
            InsuranceExpiry = cmd.InsuranceExpiry,
            Allergies = cmd.Allergies,
            ChronicConditions = cmd.ChronicConditions,
            Notes = cmd.Notes,
        };

        await patientRepository.AddAsync(patient, ct);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("Patient {Code} created for user {UserId}", code, cmd.UserId);
        return ApiResponse<Guid>.Success(patient.Id, "Patient created successfully.");
    }
}

