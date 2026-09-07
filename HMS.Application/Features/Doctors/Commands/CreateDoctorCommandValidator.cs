using FluentValidation;

namespace HMS.Application.Features.Doctors.Commands;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.DepartmentId).NotEmpty();
        RuleFor(x => x.Specialization).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Qualification).NotEmpty().MaximumLength(500);
        RuleFor(x => x.LicenseNumber).NotEmpty().MaximumLength(50);
        RuleFor(x => x.ExperienceYears).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ConsultationFee).GreaterThanOrEqualTo(0);
    }
}
