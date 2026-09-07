using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Wards.Commands;
using HMS.Application.Features.Wards.DTOs;
using HMS.Domain.Entities;

namespace HMS.Application.Features.Wards.Services;

public class WardService(IWardRepository repo, IDepartmentRepository deptRepo, IUnitOfWork uow) : IWardService
{
    public async Task<ApiResponse<PaginatedResponse<WardListItemDto>>> GetWardsAsync(PaginationQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(q, ct);
        var dtos = items.Select(w => new WardListItemDto(w.Id, w.Name, w.WardNumber, w.WardType.ToString(),
            w.Department?.Name ?? "", w.TotalBeds, w.AvailableBeds, w.ChargePerDay ?? 0m, w.IsActive));
        return ApiResponse<PaginatedResponse<WardListItemDto>>.Success(PaginatedResponse<WardListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<WardDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var w = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Ward", id);
        return ApiResponse<WardDetailDto>.Success(new WardDetailDto(w.Id, w.Name, w.WardNumber, w.WardType.ToString(),
            w.DepartmentId, w.Department?.Name ?? "", w.TotalBeds, w.AvailableBeds, w.Description,
            w.ChargePerDay ?? 0m, w.Location, w.IsActive, w.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateWardCommand cmd, CancellationToken ct)
    {
        if (!await deptRepo.ExistsAsync(cmd.DepartmentId, ct)) throw new NotFoundException("Department", cmd.DepartmentId);
        var ward = new Ward { Name = cmd.Name, WardNumber = cmd.WardNumber,
            WardType = Enum.Parse<HMS.Domain.Enums.WardType>(cmd.WardType),
            DepartmentId = cmd.DepartmentId, TotalBeds = cmd.TotalBeds, AvailableBeds = cmd.TotalBeds,
            Description = cmd.Description, ChargePerDay = cmd.ChargePerDay, Location = cmd.Location, IsActive = true };
        await repo.AddAsync(ward, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(ward.Id, "Ward created.");
    }

    public async Task<ApiResponse> UpdateAsync(UpdateWardCommand cmd, CancellationToken ct)
    {
        var ward = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Ward", cmd.Id);
        ward.Name = cmd.Name; ward.Description = cmd.Description; ward.Location = cmd.Location;
        ward.ChargePerDay = cmd.ChargePerDay; ward.IsActive = cmd.IsActive;
        repo.Update(ward); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Ward updated.");
    }

    public async Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct)
    {
        var ward = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Ward", id);
        repo.SoftDelete(ward); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Ward deleted.");
    }
}
