using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Commands;

public class UpdateMedicalRecordCommandHandler(
    IMedicalRecordRepository repo,
    IUnitOfWork uow
) : IRequestHandler<UpdateMedicalRecordCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateMedicalRecordCommand cmd, CancellationToken ct)
    {
        var record =
            await repo.GetByIdAsync(cmd.Id, ct)
            ?? throw new NotFoundException("MedicalRecord", cmd.Id);

        record.ChiefComplaint = cmd.ChiefComplaint;
        record.Diagnosis = cmd.Diagnosis;
        record.Treatment = cmd.Treatment;
        record.Notes = cmd.Notes;
        record.FollowUpInstructions = cmd.FollowUpInstructions;
        record.FollowUpDate = cmd.FollowUpDate;

        repo.Update(record);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Medical record updated.");
    }
}
