using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Staff.Commands;

public class DeleteStaffCommandHandler(
    IStaffRepository staffRepo,
    IUnitOfWork uow,
    ICurrentUserService currentUser
) : IRequestHandler<DeleteStaffCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteStaffCommand cmd, CancellationToken ct)
    {
        var staff =
            await staffRepo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Staff", cmd.Id);
        staffRepo.SoftDelete(staff, currentUser.UserId);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Staff deleted successfully.");
    }
}
