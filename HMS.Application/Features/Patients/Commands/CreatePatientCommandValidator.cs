using FluentValidation;

namespace HMS.Application.Features.Patients.Commands;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow);
        RuleFor(x => x.Gender).NotEmpty();
        RuleFor(x => x.BloodGroup).NotEmpty();
        RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.State).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(20);
        RuleFor(x => x.EmergencyContactName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.EmergencyContactPhone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.EmergencyContactRelation).NotEmpty().MaximumLength(50);
    }
}
