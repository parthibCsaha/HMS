using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.DTOs;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Patients.Queries;

public class GetPatientByIdQueryHandler(
    IPatientRepository patientRepository,
    IUserRepository userRepository
) : IRequestHandler<GetPatientByIdQuery, ApiResponse<PatientDetailDto>>
{
    public async Task<ApiResponse<PatientDetailDto>> Handle(
        GetPatientByIdQuery request,
        CancellationToken ct
    )
    {
        var patient =
            await patientRepository.GetByIdAsync(request.Id, ct)
            ?? throw new NotFoundException("Patient", request.Id);

        if (patient.User is null)
        {
            var user = await userRepository.GetByIdAsync(patient.UserId, ct);
            patient.User = user!;
        }

        return ApiResponse<PatientDetailDto>.Success(MapToDetail(patient));
    }

    private static PatientDetailDto MapToDetail(Patient p) =>
        new(
            p.Id,
            p.UserId,
            p.PatientCode,
            p.User.FirstName,
            p.User.LastName,
            p.User.Email,
            p.User.Phone,
            p.DateOfBirth,
            p.Age,
            p.Gender.ToString(),
            p.BloodGroup.ToString(),
            p.Address,
            p.City,
            p.State,
            p.Country,
            p.PostalCode,
            p.EmergencyContactName,
            p.EmergencyContactPhone,
            p.EmergencyContactRelation,
            p.InsuranceProvider,
            p.InsurancePolicyNumber,
            p.InsuranceExpiry,
            p.Allergies,
            p.ChronicConditions,
            p.Notes,
            p.IsAdmitted,
            p.CreatedAt,
            p.UpdatedAt
        );
}
