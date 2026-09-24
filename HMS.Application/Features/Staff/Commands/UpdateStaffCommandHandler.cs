using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public class UpdateStaffCommandHandler(
    IStaffRepository staffRepo,
    IUnitOfWork uow
) : IRequestHandler<UpdateStaffCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateStaffCommand cmd, CancellationToken ct)
    {
        var staff =
            await staffRepo.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException("Staff", cmd.Id);

        staff.StaffType = Enum.Parse<HMS.Domain.Enums.StaffType>(cmd.StaffType);
        staff.DepartmentId = cmd.DepartmentId;
        staff.WardId = cmd.WardId;
        staff.Qualification = cmd.Qualification;
        staff.Shift = cmd.Shift;
        staff.IsActive = cmd.IsActive;

        staffRepo.Update(staff);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Staff updated successfully.");
    }
}
