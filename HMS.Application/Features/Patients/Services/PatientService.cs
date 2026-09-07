using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.DTOs;
using HMS.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Patients.Services;

public interface IPatientService
{
    Task<ApiResponse<PaginatedResponse<PatientListItemDto>>> GetPatientsAsync(PaginationQuery query, CancellationToken ct);
    Task<ApiResponse<PatientDetailDto>> GetPatientByIdAsync(Guid id, CancellationToken ct);
    Task<ApiResponse<Guid>> CreatePatientAsync(Guid userId, DateTime dateOfBirth, string gender, string bloodGroup,
        string address, string city, string state, string country, string postalCode,
        string emergencyContactName, string emergencyContactPhone, string emergencyContactRelation,
        string? insuranceProvider, string? insurancePolicyNumber, DateTime? insuranceExpiry,
        string? allergies, string? chronicConditions, string? notes, CancellationToken ct);
    Task<ApiResponse> UpdatePatientAsync(Guid id, DateTime dateOfBirth, string gender, string bloodGroup,
        string address, string city, string state, string country, string postalCode,
        string emergencyContactName, string emergencyContactPhone, string emergencyContactRelation,
        string? insuranceProvider, string? insurancePolicyNumber, DateTime? insuranceExpiry,
        string? allergies, string? chronicConditions, string? notes, CancellationToken ct);
    Task<ApiResponse> DeletePatientAsync(Guid id, CancellationToken ct);
}

public class PatientService(
    IPatientRepository patientRepository,
    IUserRepository userRepository,
    ICodeGeneratorService codeGenerator,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUser,
    ILogger<PatientService> logger) : IPatientService
{
    public async Task<ApiResponse<PaginatedResponse<PatientListItemDto>>> GetPatientsAsync(PaginationQuery query, CancellationToken ct)
    {
        var (items, totalCount) = await patientRepository.GetPagedAsync(query, ct);
        var dtos = items.Select(p => new PatientListItemDto(
            p.Id, p.PatientCode, p.User.FirstName, p.User.LastName,
            p.User.Email, p.User.Phone, p.DateOfBirth, p.Age,
            p.Gender.ToString(), p.BloodGroup.ToString(), p.City, p.IsAdmitted));
        return ApiResponse<PaginatedResponse<PatientListItemDto>>.Success(
            PaginatedResponse<PatientListItemDto>.Create(dtos, query.PageNumber, query.PageSize, totalCount));
    }

    public async Task<ApiResponse<PatientDetailDto>> GetPatientByIdAsync(Guid id, CancellationToken ct)
    {
        var p = await patientRepository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Patient", id);

        // Ensure User is loaded
        if (p.User is null)
        {
            var user = await userRepository.GetByIdAsync(p.UserId, ct);
            p.User = user!;
        }

        return ApiResponse<PatientDetailDto>.Success(MapToDetail(p));
    }

    public async Task<ApiResponse<Guid>> CreatePatientAsync(Guid userId, DateTime dateOfBirth, string gender, string bloodGroup,
        string address, string city, string state, string country, string postalCode,
        string emergencyContactName, string emergencyContactPhone, string emergencyContactRelation,
        string? insuranceProvider, string? insurancePolicyNumber, DateTime? insuranceExpiry,
        string? allergies, string? chronicConditions, string? notes, CancellationToken ct)
    {
        if (!await userRepository.ExistsAsync(userId, ct))
            throw new NotFoundException("User", userId);

        var code = await codeGenerator.GenerateCodeAsync("PAT", ct);

        var patient = new Patient
        {
            UserId = userId,
            PatientCode = code,
            DateOfBirth = dateOfBirth,
            Gender = Enum.Parse<HMS.Domain.Enums.Gender>(gender),
            BloodGroup = Enum.Parse<HMS.Domain.Enums.BloodGroup>(bloodGroup),
            Address = address,
            City = city,
            State = state,
            Country = country,
            PostalCode = postalCode,
            EmergencyContactName = emergencyContactName,
            EmergencyContactPhone = emergencyContactPhone,
            EmergencyContactRelation = emergencyContactRelation,
            InsuranceProvider = insuranceProvider,
            InsurancePolicyNumber = insurancePolicyNumber,
            InsuranceExpiry = insuranceExpiry,
            Allergies = allergies,
            ChronicConditions = chronicConditions,
            Notes = notes
        };

        await patientRepository.AddAsync(patient, ct);
        await unitOfWork.SaveChangesAsync(ct);

        logger.LogInformation("Patient {Code} created for user {UserId}", code, userId);
        return ApiResponse<Guid>.Success(patient.Id, "Patient created successfully.");
    }

    public async Task<ApiResponse> UpdatePatientAsync(Guid id, DateTime dateOfBirth, string gender, string bloodGroup,
        string address, string city, string state, string country, string postalCode,
        string emergencyContactName, string emergencyContactPhone, string emergencyContactRelation,
        string? insuranceProvider, string? insurancePolicyNumber, DateTime? insuranceExpiry,
        string? allergies, string? chronicConditions, string? notes, CancellationToken ct)
    {
        var patient = await patientRepository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Patient", id);

        patient.DateOfBirth = dateOfBirth;
        patient.Gender = Enum.Parse<HMS.Domain.Enums.Gender>(gender);
        patient.BloodGroup = Enum.Parse<HMS.Domain.Enums.BloodGroup>(bloodGroup);
        patient.Address = address;
        patient.City = city;
        patient.State = state;
        patient.Country = country;
        patient.PostalCode = postalCode;
        patient.EmergencyContactName = emergencyContactName;
        patient.EmergencyContactPhone = emergencyContactPhone;
        patient.EmergencyContactRelation = emergencyContactRelation;
        patient.InsuranceProvider = insuranceProvider;
        patient.InsurancePolicyNumber = insurancePolicyNumber;
        patient.InsuranceExpiry = insuranceExpiry;
        patient.Allergies = allergies;
        patient.ChronicConditions = chronicConditions;
        patient.Notes = notes;

        patientRepository.Update(patient);
        await unitOfWork.SaveChangesAsync(ct);

        return ApiResponse.Success("Patient updated successfully.");
    }

    public async Task<ApiResponse> DeletePatientAsync(Guid id, CancellationToken ct)
    {
        var patient = await patientRepository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Patient", id);

        patientRepository.SoftDelete(patient, currentUser.UserId);
        await unitOfWork.SaveChangesAsync(ct);

        return ApiResponse.Success("Patient deleted successfully.");
    }

    private static PatientDetailDto MapToDetail(Patient p) => new(
        p.Id, p.UserId, p.PatientCode, p.User.FirstName, p.User.LastName,
        p.User.Email, p.User.Phone, p.DateOfBirth, p.Age,
        p.Gender.ToString(), p.BloodGroup.ToString(), p.Address, p.City,
        p.State, p.Country, p.PostalCode,
        p.EmergencyContactName, p.EmergencyContactPhone, p.EmergencyContactRelation,
        p.InsuranceProvider, p.InsurancePolicyNumber, p.InsuranceExpiry,
        p.Allergies, p.ChronicConditions, p.Notes,
        p.IsAdmitted, p.CreatedAt, p.UpdatedAt);
}
