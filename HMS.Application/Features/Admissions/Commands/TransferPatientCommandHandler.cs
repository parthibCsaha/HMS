using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Enums;
using MediatR;

namespace HMS.Application.Features.Admissions.Commands;

public class TransferPatientCommandHandler(
    IAdmissionRecordRepository repo,
    IBedRepository bedRepo,
    IUnitOfWork uow
) : IRequestHandler<TransferPatientCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(TransferPatientCommand cmd, CancellationToken ct)
    {
        var record =
            await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Admission", cmd.Id);
        var oldBed = await bedRepo.GetByIdAsync(record.BedId, ct);

        if (oldBed != null)
        {
            oldBed.Status = BedStatus.Available;
            oldBed.CurrentPatientId = null;
            bedRepo.Update(oldBed);
        }

        var newBed =
            await bedRepo.GetByIdAsync(cmd.NewBedId, ct)
            ?? throw new NotFoundException("Bed", cmd.NewBedId);
        if (newBed.Status != BedStatus.Available)
        {
            throw new BadRequestException("New bed is not available.");
        }

        newBed.Status = BedStatus.Occupied;
        newBed.CurrentPatientId = record.PatientId;
        newBed.OccupiedAt = DateTime.UtcNow;
        bedRepo.Update(newBed);

        record.WardId = cmd.NewWardId;
        record.BedId = cmd.NewBedId;
        repo.Update(record);

        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Patient transferred.");
    }
}

