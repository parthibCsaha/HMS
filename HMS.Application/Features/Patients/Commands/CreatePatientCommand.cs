using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public record CreatePatientCommand(
    Guid UserId,
    DateTime DateOfBirth,
    string Gender,
    string BloodGroup,
    string Address,
    string City,
    string State,
    string Country,
    string PostalCode,
    string EmergencyContactName,
    string EmergencyContactPhone,
    string EmergencyContactRelation,
    string? InsuranceProvider,
    string? InsurancePolicyNumber,
    DateTime? InsuranceExpiry,
    string? Allergies,
    string? ChronicConditions,
    string? Notes
) : IRequest<ApiResponse<Guid>>;
