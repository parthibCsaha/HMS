using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.Services;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public class CreatePatientCommandHandler(IPatientService service) : IRequestHandler<CreatePatientCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreatePatientCommand r, CancellationToken ct)
        => await service.CreatePatientAsync(r.UserId, r.DateOfBirth, r.Gender, r.BloodGroup,
            r.Address, r.City, r.State, r.Country, r.PostalCode,
            r.EmergencyContactName, r.EmergencyContactPhone, r.EmergencyContactRelation,
            r.InsuranceProvider, r.InsurancePolicyNumber, r.InsuranceExpiry,
            r.Allergies, r.ChronicConditions, r.Notes, ct);
}
