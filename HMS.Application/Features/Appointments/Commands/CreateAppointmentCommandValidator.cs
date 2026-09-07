using FluentValidation;

namespace HMS.Application.Features.Appointments.Commands;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.DepartmentId).NotEmpty();
        RuleFor(x => x.AppointmentDate).NotEmpty();
        RuleFor(x => x.DurationMinutes).GreaterThan(0);
    }
}
