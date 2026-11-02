using FluentValidation;
using HMS.Domain.Enums;

namespace HMS.Application.Features.Patients.Commands;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.UtcNow);

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(v => Enum.TryParse<Gender>(v, ignoreCase: true, out _))
            .WithMessage("Invalid gender. Allowed values: Male, Female, Other.");

        RuleFor(x => x.BloodGroup)
            .NotEmpty()
            .Must(v => Enum.TryParse<BloodGroup>(v, ignoreCase: true, out _))
            .WithMessage("Invalid blood group. Allowed values: APositive, ANegative, BPositive, BNegative, ABPositive, ABNegative, OPositive, ONegative.");

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
