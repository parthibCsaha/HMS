using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Interfaces;
using HMS.Application.Common.Interfaces.Repositories;
using HMS.Application.Common.Interfaces.Services;
using HMS.Application.Common.Models;
using HMS.Application.Features.Staff.Commands;
using HMS.Application.Features.Staff.DTOs;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Features.Staff.Services;

public class StaffService(IStaffRepository staffRepo, IUserRepository userRepo,
    ICodeGeneratorService codeGen, IUnitOfWork uow, ICurrentUserService currentUser, ILogger<StaffService> logger) : IStaffService
{
    public async Task<ApiResponse<PaginatedResponse<StaffListItemDto>>> GetStaffAsync(PaginationQuery query, CancellationToken ct)
    {
        var (items, total) = await staffRepo.GetPagedAsync(query, ct);
        var dtos = items.Select(s => new StaffListItemDto(s.Id, s.StaffCode, s.User.FirstName, s.User.LastName,
            s.User.Email, s.StaffType.ToString(), s.Department?.Name, s.IsActive));
        return ApiResponse<PaginatedResponse<StaffListItemDto>>.Success(PaginatedResponse<StaffListItemDto>.Create(dtos, query.PageNumber, query.PageSize, total));
    }

    public async Task<ApiResponse<StaffDetailDto>> GetStaffByIdAsync(Guid id, CancellationToken ct)
    {
        var s = await staffRepo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Staff", id);
        if (s.User is null) s.User = (await userRepo.GetByIdAsync(s.UserId, ct))!;
        return ApiResponse<StaffDetailDto>.Success(new StaffDetailDto(s.Id, s.UserId, s.StaffCode,
            s.User.FirstName, s.User.LastName, s.User.Email, s.User.Phone,
            s.StaffType.ToString(), s.DepartmentId, s.Department?.Name,
            s.WardId, s.Ward?.Name, s.Qualification, s.JoiningDate, s.Shift,
            s.IsActive, s.CreatedAt, s.UpdatedAt));
    }

    public async Task<ApiResponse<Guid>> CreateStaffAsync(CreateStaffCommand cmd, CancellationToken ct)
    {
        if (!await userRepo.ExistsAsync(cmd.UserId, ct)) throw new NotFoundException("User", cmd.UserId);
        var code = await codeGen.GenerateCodeAsync("STF", ct);
        var staff = new Domain.Entities.Staff
        {
            UserId = cmd.UserId, StaffCode = code,
            StaffType = Enum.Parse<HMS.Domain.Enums.StaffType>(cmd.StaffType),
            DepartmentId = cmd.DepartmentId, WardId = cmd.WardId,
            Qualification = cmd.Qualification, JoiningDate = cmd.JoiningDate,
            Shift = cmd.Shift, IsActive = true
        };
        await staffRepo.AddAsync(staff, ct);
        await uow.SaveChangesAsync(ct);
        logger.LogInformation("Staff {Code} created", code);
        return ApiResponse<Guid>.Success(staff.Id, "Staff created successfully.");
    }

    public async Task<ApiResponse> UpdateStaffAsync(UpdateStaffCommand cmd, CancellationToken ct)
    {
        var staff = await staffRepo.GetByIdAsync(cmd.Id, ct) ?? throw new NotFoundException("Staff", cmd.Id);
        staff.StaffType = Enum.Parse<HMS.Domain.Enums.StaffType>(cmd.StaffType);
        staff.DepartmentId = cmd.DepartmentId; staff.WardId = cmd.WardId;
        staff.Qualification = cmd.Qualification; staff.Shift = cmd.Shift; staff.IsActive = cmd.IsActive;
        staffRepo.Update(staff);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Staff updated successfully.");
    }

    public async Task<ApiResponse> DeleteStaffAsync(Guid id, CancellationToken ct)
    {
        var staff = await staffRepo.GetByIdAsync(id, ct) ?? throw new NotFoundException("Staff", id);
        staffRepo.SoftDelete(staff, currentUser.UserId);
        await uow.SaveChangesAsync(ct);
        return ApiResponse.Success("Staff deleted successfully.");
    }
}
