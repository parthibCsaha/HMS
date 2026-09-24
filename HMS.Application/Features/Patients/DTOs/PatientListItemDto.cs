namespace HMS.Application.Features.Patients.DTOs;

public record PatientListItemDto(
    Guid Id,
    string PatientCode,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime DateOfBirth,
    int Age,
    string Gender,
    string BloodGroup,
    string City,
    bool IsAdmitted
);
