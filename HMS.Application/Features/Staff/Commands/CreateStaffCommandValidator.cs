using FluentValidation;

namespace HMS.Application.Features.Staff.Commands;

public class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
{
    public CreateStaffCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.StaffType).NotEmpty();
        RuleFor(x => x.Qualification).NotEmpty().MaximumLength(500);
        RuleFor(x => x.JoiningDate).NotEmpty();
    }
}
