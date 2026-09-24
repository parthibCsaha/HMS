using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public record UpdatePatientCommand(
    Guid Id,
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
) : IRequest<ApiResponse>;
