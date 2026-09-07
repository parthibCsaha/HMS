using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.LabOrders.Commands;
using HMS.Application.Features.LabOrders.DTOs;
using HMS.Domain.Entities;
using HMS.Domain.Enums;

namespace HMS.Application.Features.LabOrders.Services;

public class LabOrderService(ILabOrderRepository repo, ILabResultRepository resultRepo,
    ICodeGeneratorService codeGen, IUnitOfWork uow) : ILabOrderService
{
    public async Task<ApiResponse<PaginatedResponse<LabOrderListItemDto>>> GetLabOrdersAsync(PaginationQuery q, Guid? patientId, LabTestStatus? status, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(q, patientId, status, ct);
        var dtos = items.Select(lo => new LabOrderListItemDto(lo.Id, lo.OrderCode,
            $"{lo.Patient.User.FirstName} {lo.Patient.User.LastName}",
            $"{lo.OrderingDoctor.User.FirstName} {lo.OrderingDoctor.User.LastName}",
            lo.Status.ToString(), lo.OrderDate, lo.Items.Count));
        return ApiResponse<PaginatedResponse<LabOrderListItemDto>>.Success(PaginatedResponse<LabOrderListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<LabOrderDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var lo = await repo.GetWithItemsAsync(id, ct) ?? throw new NotFoundException("LabOrder", id);
        var items = lo.Items.Select(i => new LabOrderItemDto(i.Id, i.LabTest?.Name ?? "", i.LabTest?.Code ?? "",
            i.Status.ToString(), i.Price,
            i.Result != null ? new LabResultDto(i.Result.Id, i.Result.Result, i.Result.Unit,
                i.Result.ReferenceRange, i.Result.Interpretation, i.Result.ResultedAt, i.Result.Notes, i.Result.IsAbnormal ?? false) : null)).ToList();
        return ApiResponse<LabOrderDetailDto>.Success(new LabOrderDetailDto(lo.Id, lo.OrderCode, lo.PatientId,
            $"{lo.Patient.User.FirstName} {lo.Patient.User.LastName}", lo.OrderingDoctorId,
            $"{lo.OrderingDoctor.User.FirstName} {lo.OrderingDoctor.User.LastName}",
            lo.Status.ToString(), lo.OrderDate, lo.ClinicalNotes, lo.Priority,
            lo.IsBilled, items, lo.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateLabOrderCommand cmd, CancellationToken ct)
    {
        var code = await codeGen.GenerateCodeAsync("LAB", ct);
        var order = new LabOrder
        {
            OrderCode = code, PatientId = cmd.PatientId, OrderingDoctorId = cmd.OrderingDoctorId,
            MedicalRecordId = cmd.MedicalRecordId, AdmissionId = cmd.AdmissionId,
            OrderDate = DateTime.UtcNow, Status = LabTestStatus.Pending,
            ClinicalNotes = cmd.ClinicalNotes, Priority = cmd.Priority ?? "Routine"
        };
        foreach (var item in cmd.Items)
        {
            order.Items.Add(new LabOrderItem { LabTestId = item.LabTestId, Price = item.Price, Status = LabTestStatus.Pending });
        }
        await repo.AddAsync(order, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(order.Id, "Lab order created.");
    }

    public async Task<ApiResponse> AddResultAsync(AddLabResultCommand cmd, CancellationToken ct)
    {
        var result = new LabResult
        {
            LabOrderId = cmd.LabOrderId, LabOrderItemId = cmd.LabOrderItemId,
            LabTestId = cmd.LabTestId, PatientId = cmd.PatientId,
            Result = cmd.Result, Unit = cmd.Unit, ReferenceRange = cmd.ReferenceRange,
            Interpretation = cmd.Interpretation, IsAbnormal = cmd.IsAbnormal,
            ResultedAt = DateTime.UtcNow, ResultedBy = cmd.ResultedBy, Notes = cmd.Notes
        };
        await resultRepo.AddAsync(result, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Lab result added.");
    }
}
