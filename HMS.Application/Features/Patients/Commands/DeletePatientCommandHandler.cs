using HMS.Application.Common.Models;
using HMS.Application.Features.Patients.Services;
using MediatR;

namespace HMS.Application.Features.Patients.Commands;

public class DeletePatientCommandHandler(IPatientService service) : IRequestHandler<DeletePatientCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeletePatientCommand r, CancellationToken ct)
        => await service.DeletePatientAsync(r.Id, ct);
}
