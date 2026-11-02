using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.DTOs;
using MediatR;

namespace HMS.Application.Features.Staff.Queries;

public class GetStaffByIdQueryHandler(
    IStaffRepository staffRepo
) : IRequestHandler<GetStaffByIdQuery, ApiResponse<StaffDetailDto>>
{
    public async Task<ApiResponse<StaffDetailDto>> Handle(
        GetStaffByIdQuery request,
        CancellationToken ct
    )
    {
        var staff =
            await staffRepo.GetByIdAsync(request.Id, ct)
            ?? throw new NotFoundException("Staff", request.Id);

        // User, Department, Ward are now included by the repository — no N+1 workaround needed.
        var dto = new StaffDetailDto(
            staff.Id,
            staff.UserId,
            staff.StaffCode,
            staff.User.FirstName,
            staff.User.LastName,
            staff.User.Email,
            staff.User.Phone,
            staff.StaffType.ToString(),
            staff.DepartmentId,
            staff.Department?.Name,
            staff.WardId,
            staff.Ward?.Name,
            staff.Qualification,
            staff.JoiningDate,
            staff.Shift,
            staff.IsActive,
            staff.CreatedAt,
            staff.UpdatedAt
        );

        return ApiResponse<StaffDetailDto>.Success(dto);
    }
}
