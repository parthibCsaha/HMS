using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Wards.Commands;

public class CreateWardCommandHandler(
    IWardRepository repo,
    IDepartmentRepository deptRepo,
    IUnitOfWork uow
) : IRequestHandler<CreateWardCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateWardCommand cmd, CancellationToken ct)
    {
        if (!await deptRepo.ExistsAsync(cmd.DepartmentId, ct))
        {
            throw new NotFoundException("Department", cmd.DepartmentId);
        }
        var ward = new Ward
        {
            Name = cmd.Name,
            WardNumber = cmd.WardNumber,
            WardType = Enum.Parse<HMS.Domain.Enums.WardType>(cmd.WardType),
            DepartmentId = cmd.DepartmentId,
            TotalBeds = cmd.TotalBeds,
            AvailableBeds = cmd.TotalBeds,
            Description = cmd.Description,
            ChargePerDay = cmd.ChargePerDay,
            Location = cmd.Location,
            IsActive = true,
        };
        await repo.AddAsync(ward, ct);
        await uow.SaveChangesAsync(ct);

        return ApiResponse<Guid>.Success(ward.Id, "Ward created.");
    }
}
