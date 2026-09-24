using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.LabOrders.Commands;

public class AddLabResultCommandHandler(
    ILabResultRepository resultRepo,
    IUnitOfWork uow
) : IRequestHandler<AddLabResultCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(AddLabResultCommand cmd, CancellationToken ct)
    {
        var result = new LabResult
        {
            LabOrderId = cmd.LabOrderId,
            LabOrderItemId = cmd.LabOrderItemId,
            LabTestId = cmd.LabTestId,
            PatientId = cmd.PatientId,
            Result = cmd.Result,
            Unit = cmd.Unit,
            ReferenceRange = cmd.ReferenceRange,
            Interpretation = cmd.Interpretation,
            IsAbnormal = cmd.IsAbnormal,
            ResultedAt = DateTime.UtcNow,
            ResultedBy = cmd.ResultedBy,
            Notes = cmd.Notes,
        };
        await resultRepo.AddAsync(result, ct);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Lab result added.");
    }
}
