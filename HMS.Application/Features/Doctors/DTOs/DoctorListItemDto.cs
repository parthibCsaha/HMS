namespace HMS.Application.Features.Doctors.DTOs;

public record DoctorListItemDto(Guid Id, string DoctorCode, string FirstName, string LastName,
    string Email, string Specialization, string DepartmentName, decimal ConsultationFee,
    bool IsAvailable, int ExperienceYears);
