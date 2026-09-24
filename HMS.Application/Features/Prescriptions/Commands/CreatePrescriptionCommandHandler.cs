using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Prescriptions.Commands;

public class CreatePrescriptionCommandHandler(
    IPrescriptionRepository repo,
    HMS.Application.Common.Interfaces.Services.ICodeGeneratorService codeGen,
    IUnitOfWork uow
) : IRequestHandler<CreatePrescriptionCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreatePrescriptionCommand cmd, CancellationToken ct)
    {
        var code = await codeGen.GenerateCodeAsync("RX", ct);
        var prescription = new Prescription
        {
            PrescriptionCode = code,
            MedicalRecordId = cmd.MedicalRecordId,
            PatientId = cmd.PatientId,
            DoctorId = cmd.DoctorId,
            IssuedDate = DateTime.UtcNow,
            ExpiryDate = cmd.ExpiryDate,
            Instructions = cmd.Instructions,
            Notes = cmd.Notes,
        };
        foreach (var item in cmd.Items)
        {
            prescription.Items.Add(
                new PrescriptionItem
                {
                    MedicationId = item.MedicationId,
                    Dosage = item.Dosage,
                    Frequency = item.Frequency,
                    Route = item.Route,
                    DurationDays = item.DurationDays,
                    Quantity = item.Quantity,
                    Instructions = item.Instructions,
                }
            );
        }
        await repo.AddAsync(prescription, ct);
        await uow.SaveChangesAsync(ct);

        return ApiResponse<Guid>.Success(prescription.Id, "Prescription created.");
    }
}
