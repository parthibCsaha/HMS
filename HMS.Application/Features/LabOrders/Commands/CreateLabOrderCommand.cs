using HMS.Application.Common.Models;
using HMS.Application.Common.Security;
using MediatR;

namespace HMS.Application.Features.LabOrders.Commands;

public record CreateLabOrderItemInput(Guid LabTestId, decimal Price);

[RequirePermission(Permissions.LabOrdersCreate)]
public record CreateLabOrderCommand(
    Guid PatientId,
    Guid OrderingDoctorId,
    Guid? MedicalRecordId,
    Guid? AdmissionId,
    string? ClinicalNotes,
    string? Priority,
    List<CreateLabOrderItemInput> Items
) : IRequest<ApiResponse<Guid>>;
