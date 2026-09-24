using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Models;
using HMS.Domain.Entities;
using MediatR;

namespace HMS.Application.Features.Departments.Commands;

public class CreateDepartmentCommandHandler(
    IDepartmentRepository repo,
    ICodeGeneratorService codeGen,
    IUnitOfWork uow
) : IRequestHandler<CreateDepartmentCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> Handle(CreateDepartmentCommand cmd, CancellationToken ct)
    {
        if (await repo.NameExistsAsync(cmd.Name, null, ct))
            throw new ConflictException($"Department '{cmd.Name}' already exists.");
        var code = await codeGen.GenerateCodeAsync("DEPT", ct);
        var dept = new Department
        {
            Name = cmd.Name,
            Code = code,
            Description = cmd.Description,
            HeadDoctorId = cmd.HeadDoctorId,
            Location = cmd.Location,
            Phone = cmd.Phone,
            Email = cmd.Email,
            IsActive = true,
        };
        await repo.AddAsync(dept, ct);
        await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(dept.Id, "Department created.");
    }
}

