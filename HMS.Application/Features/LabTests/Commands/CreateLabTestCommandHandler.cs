using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.LabTests.Commands;

public class CreateLabTestCommandHandler(ILabTestRepository repo, IUnitOfWork uow)
    : IRequestHandler<CreateLabTestCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateLabTestCommand cmd, CancellationToken ct)
    {
        if (await repo.CodeExistsAsync(cmd.Code, ct))
            throw new ConflictException($"Lab test code '{cmd.Code}' already exists.");
        var test = new LabTest
        {
            Code = cmd.Code,
            Name = cmd.Name,
            Category = cmd.Category,
            Description = cmd.Description,
            Price = cmd.Price,
            SampleType = cmd.SampleType,
            ReferenceRange = cmd.ReferenceRange,
            Unit = cmd.Unit,
            TurnaroundTimeHours = cmd.TurnaroundTimeHours,
            IsActive = true,
        };
        await repo.AddAsync(test, ct);
        await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(test.Id, "Lab test created.");
    }
}
