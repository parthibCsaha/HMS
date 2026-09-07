using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Prescriptions.Commands;
using HMS.Application.Features.Prescriptions.DTOs;
using HMS.Domain.Entities;

namespace HMS.Application.Features.Prescriptions.Services;

public class PrescriptionService(IPrescriptionRepository repo, IMedicationRepository medRepo,
    ICodeGeneratorService codeGen, IUnitOfWork uow) : IPrescriptionService
{
    public async Task<ApiResponse<PaginatedResponse<PrescriptionListItemDto>>> GetByPatientAsync(Guid patientId, PaginationQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedByPatientAsync(patientId, q, ct);
        var dtos = items.Select(p => new PrescriptionListItemDto(p.Id, p.PrescriptionCode,
            p.Doctor?.User != null ? $"{p.Doctor.User.FirstName} {p.Doctor.User.LastName}" : "",
            p.IssuedDate, p.IsDispensed, p.Items.Count));
        return ApiResponse<PaginatedResponse<PrescriptionListItemDto>>.Success(PaginatedResponse<PrescriptionListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<PrescriptionDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var p = await repo.GetWithItemsAsync(id, ct) ?? throw new NotFoundException("Prescription", id);
        var items = p.Items.Select(i => new PrescriptionItemDto(i.Id, i.Medication?.Name ?? "",
            i.Dosage, i.Frequency, i.Route, i.DurationDays, i.Quantity, i.Instructions, i.IsDispensed)).ToList();
        return ApiResponse<PrescriptionDetailDto>.Success(new PrescriptionDetailDto(p.Id, p.PrescriptionCode,
            p.PatientId, p.DoctorId, p.Doctor?.User != null ? $"{p.Doctor.User.FirstName} {p.Doctor.User.LastName}" : "",
            p.IssuedDate, p.ExpiryDate, p.Instructions, p.Notes, p.IsDispensed, p.DispensedAt, items, p.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreatePrescriptionCommand cmd, CancellationToken ct)
    {
        var code = await codeGen.GenerateCodeAsync("RX", ct);
        var prescription = new Prescription
        {
            PrescriptionCode = code, MedicalRecordId = cmd.MedicalRecordId, PatientId = cmd.PatientId,
            DoctorId = cmd.DoctorId, IssuedDate = DateTime.UtcNow, ExpiryDate = cmd.ExpiryDate,
            Instructions = cmd.Instructions, Notes = cmd.Notes
        };
        foreach (var item in cmd.Items)
        {
            prescription.Items.Add(new PrescriptionItem
            {
                MedicationId = item.MedicationId, Dosage = item.Dosage, Frequency = item.Frequency,
                Route = item.Route, DurationDays = item.DurationDays, Quantity = item.Quantity,
                Instructions = item.Instructions
            });
        }
        await repo.AddAsync(prescription, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(prescription.Id, "Prescription created.");
    }

    public async Task<ApiResponse> DispenseAsync(Guid id, CancellationToken ct)
    {
        var p = await repo.GetWithItemsAsync(id, ct) ?? throw new NotFoundException("Prescription", id);
        p.IsDispensed = true; p.DispensedAt = DateTime.UtcNow;
        foreach (var item in p.Items)
        {
            item.IsDispensed = true;
            var med = await medRepo.GetByIdAsync(item.MedicationId, ct);
            if (med != null) { med.StockQuantity -= item.Quantity; medRepo.Update(med); }
        }
        repo.Update(p); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Prescription dispensed.");
    }
}
