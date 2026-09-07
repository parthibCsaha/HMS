using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using MediatR;

namespace HMS.Application.Features.LabTests.Commands;

public class UpdateLabTestCommandHandler(ILabTestRepository repo, IUnitOfWork uow) : IRequestHandler<UpdateLabTestCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(UpdateLabTestCommand cmd, CancellationToken ct)
    {
        var test = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("LabTest", cmd.Id);
        test.Name = cmd.Name; test.Category = cmd.Category; test.Description = cmd.Description;
        test.Price = cmd.Price; test.ReferenceRange = cmd.ReferenceRange; test.Unit = cmd.Unit; test.IsActive = cmd.IsActive;
        repo.Update(test); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Lab test updated.");
    }
}
