using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.LabOrders.Commands;

public record CreateLabOrderItemInput(Guid LabTestId, decimal Price);

public record CreateLabOrderCommand(Guid PatientId, Guid OrderingDoctorId, Guid? MedicalRecordId,
    Guid? AdmissionId, string? ClinicalNotes, string? Priority,
    List<CreateLabOrderItemInput> Items) : IRequest<ApiResponse<Guid>>;
