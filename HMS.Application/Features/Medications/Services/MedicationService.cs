using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Application.Features.Medications.Commands;
using HMS.Application.Features.Medications.DTOs;
using HMS.Domain.Entities;

namespace HMS.Application.Features.Medications.Services;

public class MedicationService(IMedicationRepository repo, IUnitOfWork uow) : IMedicationService
{
    public async Task<ApiResponse<PaginatedResponse<MedicationListItemDto>>> GetMedicationsAsync(PaginationQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(q, ct);
        var dtos = items.Select(m => new MedicationListItemDto(m.Id, m.Name, m.GenericName, m.Category,
            m.UnitPrice, m.StockQuantity, m.ReorderLevel, m.IsActive));
        return ApiResponse<PaginatedResponse<MedicationListItemDto>>.Success(PaginatedResponse<MedicationListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateMedicationCommand cmd, CancellationToken ct)
    {
        var med = new Medication
        {
            Name = cmd.Name, GenericName = cmd.GenericName, Category = cmd.Category,
            Manufacturer = cmd.Manufacturer, DosageForm = cmd.DosageForm, Strength = cmd.Strength,
            UnitPrice = cmd.UnitPrice, StockQuantity = cmd.StockQuantity, ReorderLevel = cmd.ReorderLevel,
            ExpiryDate = cmd.ExpiryDate, IsActive = true
        };
        await repo.AddAsync(med, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(med.Id, "Medication created.");
    }

    public async Task<ApiResponse> UpdateAsync(UpdateMedicationCommand cmd, CancellationToken ct)
    {
        var med = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Medication", cmd.Id);
        med.Name = cmd.Name; med.GenericName = cmd.GenericName; med.Category = cmd.Category;
        med.UnitPrice = cmd.UnitPrice; med.StockQuantity = cmd.StockQuantity; med.ReorderLevel = cmd.ReorderLevel;
        med.IsActive = cmd.IsActive;
        repo.Update(med); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Medication updated.");
    }

    public async Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct)
    {
        var med = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Medication", id);
        repo.SoftDelete(med); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Medication deleted.");
    }

    public async Task<ApiResponse<IEnumerable<MedicationListItemDto>>> GetLowStockAsync(CancellationToken ct)
    {
        var items = await repo.GetLowStockAsync(ct);
        var dtos = items.Select(m => new MedicationListItemDto(m.Id, m.Name, m.GenericName, m.Category,
            m.UnitPrice, m.StockQuantity, m.ReorderLevel, m.IsActive));
        return ApiResponse<IEnumerable<MedicationListItemDto>>.Success(dtos);
    }
}
