using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.Services;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Commands;

public class UpdateMedicalRecordCommandHandler(IMedicalRecordService svc) : IRequestHandler<UpdateMedicalRecordCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateMedicalRecordCommand r, CancellationToken ct)
        => await svc.UpdateAsync(r, ct);
}
