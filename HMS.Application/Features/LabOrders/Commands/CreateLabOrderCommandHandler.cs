using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.LabOrders.Commands;

public class CreateLabOrderCommandHandler(
    ILabOrderRepository repo,
    HMS.Application.Common.Interfaces.Services.ICodeGeneratorService codeGen,
    IUnitOfWork uow
) : IRequestHandler<CreateLabOrderCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateLabOrderCommand cmd, CancellationToken ct)
    {
        var code = await codeGen.GenerateCodeAsync("LAB", ct);
        var order = new LabOrder
        {
            OrderCode = code,
            PatientId = cmd.PatientId,
            OrderingDoctorId = cmd.OrderingDoctorId,
            MedicalRecordId = cmd.MedicalRecordId,
            AdmissionId = cmd.AdmissionId,
            OrderDate = DateTime.UtcNow,
            Status = LabTestStatus.Pending,
            ClinicalNotes = cmd.ClinicalNotes,
            Priority = cmd.Priority ?? "Routine",
        };
        foreach (var item in cmd.Items)
        {
            order.Items.Add(
                new LabOrderItem
                {
                    LabTestId = item.LabTestId,
                    Price = item.Price,
                    Status = LabTestStatus.Pending,
                }
            );
        }

        await repo.AddAsync(order, ct);
        await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(order.Id, "Lab order created.");
    }
}
