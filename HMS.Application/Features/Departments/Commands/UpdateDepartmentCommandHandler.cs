using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public class UpdateDepartmentCommandHandler(
    IDepartmentRepository repo,
    IUnitOfWork uow
) : IRequestHandler<UpdateDepartmentCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateDepartmentCommand cmd, CancellationToken ct)
    {
        var dept =
            await repo.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException("Department", cmd.Id);

        if (await repo.NameExistsAsync(cmd.Name, cmd.Id, ct))
        {
            throw new ConflictException($"Department '{cmd.Name}' already exists.");
        }

        dept.Name = cmd.Name;
        dept.Description = cmd.Description;
        dept.HeadDoctorId = cmd.HeadDoctorId;
        dept.Location = cmd.Location;
        dept.Phone = cmd.Phone;
        dept.Email = cmd.Email;
        dept.IsActive = cmd.IsActive;

        repo.Update(dept);
        await uow.SaveChangesAsync(ct);

        return ApiResponse.Success("Department updated.");
    }
}
