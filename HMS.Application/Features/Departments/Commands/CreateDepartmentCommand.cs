using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public record CreateDepartmentCommand(
    string Name,
    string? Description,
    Guid? HeadDoctorId,
    string? Location,
    string? Phone,
    string? Email
) : IRequest<ApiResponse<Guid>>;
