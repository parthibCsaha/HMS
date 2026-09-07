using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Departments.Commands;
using HMS.Application.Features.Departments.DTOs;
using HMS.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Departments.Services;

public class DepartmentService(IDepartmentRepository repo, ICodeGeneratorService codeGen,
    IUnitOfWork uow, ICurrentUserService cur, ILogger<DepartmentService> log) : IDepartmentService
{
    public async Task<ApiResponse<PaginatedResponse<DepartmentListItemDto>>> GetDepartmentsAsync(PaginationQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.GetPagedAsync(q, ct);
        var dtos = items.Select(d => new DepartmentListItemDto(d.Id, d.Code, d.Name,
            d.HeadDoctor != null ? $"{d.HeadDoctor.User.FirstName} {d.HeadDoctor.User.LastName}" : null, d.Location, d.IsActive));
        return ApiResponse<PaginatedResponse<DepartmentListItemDto>>.Success(PaginatedResponse<DepartmentListItemDto>.Create(dtos, q.PageNumber, q.PageSize, total));
    }

    public async Task<ApiResponse<DepartmentDetailDto>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var d = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Department", id);
        return ApiResponse<DepartmentDetailDto>.Success(new DepartmentDetailDto(d.Id, d.Code, d.Name, d.Description,
            d.HeadDoctorId, d.HeadDoctor != null ? $"{d.HeadDoctor.User.FirstName} {d.HeadDoctor.User.LastName}" : null,
            d.Location, d.Phone, d.Email, d.IsActive, d.CreatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateDepartmentCommand cmd, CancellationToken ct)
    {
        if (await repo.NameExistsAsync(cmd.Name, null, ct)) throw new ConflictException($"Department '{cmd.Name}' already exists.");
        var code = await codeGen.GenerateCodeAsync("DEPT", ct);
        var dept = new Department { Name = cmd.Name, Code = code, Description = cmd.Description,
            HeadDoctorId = cmd.HeadDoctorId, Location = cmd.Location, Phone = cmd.Phone, Email = cmd.Email, IsActive = true };
        await repo.AddAsync(dept, ct); await uow.SaveChangesAsync(ct);
        return ApiResponse<Guid>.Success(dept.Id, "Department created.");
    }

    public async Task<ApiResponse> UpdateAsync(UpdateDepartmentCommand cmd, CancellationToken ct)
    {
        var dept = await repo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Department", cmd.Id);
        if (await repo.NameExistsAsync(cmd.Name, cmd.Id, ct)) throw new ConflictException($"Department '{cmd.Name}' already exists.");
        dept.Name = cmd.Name; dept.Description = cmd.Description; dept.HeadDoctorId = cmd.HeadDoctorId;
        dept.Location = cmd.Location; dept.Phone = cmd.Phone; dept.Email = cmd.Email; dept.IsActive = cmd.IsActive;
        repo.Update(dept); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Department updated.");
    }

    public async Task<ApiResponse> DeleteAsync(Guid id, CancellationToken ct)
    {
        var dept = await repo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Department", id);
        repo.SoftDelete(dept, cur.UserId); await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Department deleted.");
    }
}
