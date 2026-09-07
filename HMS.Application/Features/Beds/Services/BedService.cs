using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Beds.Commands;
using HMS.Application.Features.Beds.DTOs;
using HMS.Domain.Entities;
using HMS.Domain.Enums;

namespace HMS.Application.Features.Beds.Services;

public class BedService(IBedRepository repo, IWardRepository wardRepo, IUnitOfWork uow) : IBedService
{
    public async Task<ApiResponse<IEnumerable<BedListItemDto>>> GetByWardAsync(Guid wardId, CancellationToken ct)
    {
        var beds = await repo.GetByWardAsync(wardId, ct);
        var dtos = beds.Select(b => new BedListItemDto(b.Id, b.BedNumber, b.Ward?.Name ?? "",
            b.Status.ToString(), b.CurrentPatient?.User != null ? $"{b.CurrentPatient.User.FirstName} {b.CurrentPatient.User.LastName}" : null));
        return ApiResponse<IEnumerable<BedListItemDto>>.Success(dtos);
    }

    public async Task<ApiResponse<BedDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var b = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Bed", id);
        return ApiResponse<BedDetailDto>.Success(new BedDetailDto(b.Id, b.BedNumber, b.WardId,
            b.Ward?.Name ?? "", b.Status.ToString(), b.CurrentPatientId, null, b.OccupiedAt, b.Notes, b.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateBedCommand cmd, CancellationToken ct)
    {
        if (!await wardRepo.ExistsAsync(cmd.WardId, ct)) throw new NotFoundException("Ward", cmd.WardId);
        var bed = new Bed { BedNumber = cmd.BedNumber, WardId = cmd.WardId, Status = BedStatus.Available, Notes = cmd.Notes };
        await repo.AddAsync(bed, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(bed.Id, "Bed created.");
    }

    public async Task<ApiResponse> UpdateStatusAsync(Guid id, string status, CancellationToken ct)
    {
        var bed = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Bed", id);
        bed.Status = Enum.Parse<BedStatus>(status);
        repo.Update(bed); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Bed status updated.");
    }

    public async Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct)
    {
        var bed = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Bed", id);
        repo.SoftDelete(bed); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Bed deleted.");
    }
}
