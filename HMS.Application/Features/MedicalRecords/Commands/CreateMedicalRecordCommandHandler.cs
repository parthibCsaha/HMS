using HMS.Application.Common.Models;
using HMS.Application.Features.MedicalRecords.Services;
using MediatR;

namespace HMS.Application.Features.MedicalRecords.Commands;

public class CreateMedicalRecordCommandHandler(IMedicalRecordService svc) : IRequestHandler<CreateMedicalRecordCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateMedicalRecordCommand r, CancellationToken ct)
        => await svc.CreateAsync(r, ct);
}
