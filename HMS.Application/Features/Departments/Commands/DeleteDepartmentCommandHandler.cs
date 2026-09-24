using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public class DeleteDepartmentCommandHandler(
    IDepartmentRepository repo,
    IUnitOfWork uow,
    ICurrentUserService cur
) : IRequestHandler<DeleteDepartmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteDepartmentCommand cmd, CancellationToken ct)
    {
        var dept = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Department", cmd.Id);

        repo.SoftDelete(dept, cur.UserId);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Department deleted.");
    }
}

