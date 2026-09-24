using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.LabOrders.Commands;

public record AddLabResultCommand(
    Guid LabOrderId,
    Guid LabOrderItemId,
    Guid LabTestId,
    Guid PatientId,
    string Result,
    string? Unit,
    string? ReferenceRange,
    string? Interpretation,
    bool IsAbnormal,
    Guid ResultedBy,
    string? Notes
) : IRequest<ApiResponse>;
